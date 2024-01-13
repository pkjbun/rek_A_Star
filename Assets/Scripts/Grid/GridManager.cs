using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;


namespace AStar
{
    public class GridManager : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] private GridSerializeData gridSerializeData;
        [SerializeField] private List<ListOfNodes> grid = new List<ListOfNodes>();
        [SerializeField] private Node nodePrefab;
        #endregion
        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            GenerateGrid();
        }

     
        #endregion
        #region Custom Methods   
        private void GenerateGrid()
        {
            if(gridSerializeData!=null)
            {
                if(gridSerializeData.Data.Count==0)
                {
                    gridSerializeData.GenerateData();
                }
                for (int y = 0; y < gridSerializeData.Data.Count; y++)
                {
                    ListOfNodes row = new ListOfNodes();
                    GridSerializeData.ListNodeData nd = gridSerializeData.Data[y];
                    for(int  x = 0; x< nd.Row.Count; x++)
                    {
                        Node node = Instantiate(nodePrefab, nd.Row[x].WorldPosition, Quaternion.identity, this.transform);
                        node.Setup(this);
                        row.Row.Add(node);
                    }
                    grid.Add(row);
                }
            }
        }

        /// <summary>
        /// Returns Grid data, use for saving data
        /// </summary>
        /// <returns></returns>
        public GridSerializeData GetGridData()
        {
            return gridSerializeData;
        }
        public void RestoreGrid(GridSerializeData griddata)
        {
            gridSerializeData = griddata;
        }
        /// <summary>
        /// Return node at specified coordinates
        /// </summary>
        /// <param name="x">x coordinate of node in Grid Layout</param>
        /// <param name="y">y coordinate of node in Grid Layout</param>
        /// <returns></returns>
        public Node GetNodeAt(int x, int y)
        {
            // Check if the x and y coordinates are within the bounds of the grid
            if (x >= 0 && x < gridSerializeData.Width && y >= 0 && y < gridSerializeData.Height)
            {
                return grid[y].Row[x];
            }
            else
            {
                // Return null to handle the case where the coordinates are out of bounds
                return null;
            }
        }
        public Stack<Node> FindPath(Node Start, Node End)
        {
            Stack<Node> Path = new Stack<Node>();
            PriorityQueue<Node, float> OpenList = new PriorityQueue<Node, float>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = Start;

            // add start node to Open List
            OpenList.Enqueue(Start, Start.CalculateMoveF());

            while (OpenList.Count != 0 && !ClosedList.Exists(x => x.GetWorldPosition() == End.GetWorldPosition()))
            {
                current = OpenList.Dequeue();
                ClosedList.Add(current);
                adjacencies = current.GetAdjacencies();

                foreach (Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.IsWalkable())
                    {
                        bool isFound = false;
                        foreach (var oLNode in OpenList.UnorderedItems)
                        {
                            if (oLNode.Element == n)
                            {
                                isFound = true;
                            }
                        }
                        if (!isFound)
                        {
                            n.Parent = current;
                            n.DistanceToTarget = Vector2.Distance(n.GetWorldPosition(), End.GetWorldPosition());
                            n.CurrrentCost = n.GetMoveCost() + n.Parent.CurrrentCost;
                            OpenList.Enqueue(n, n.CalculateMoveF());
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!ClosedList.Exists(x => x.GetWorldPosition() == End.GetWorldPosition()))
            {
                return null;
            }

            // if all good, return path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            if (temp == null) return null;
            do
            {
                Path.Push(temp);
                temp = temp.Parent;
            } while (temp != Start && temp != null);
            return Path;
        }
        #endregion
        /// <summary>
        /// Class to help serialize in Editor nad JSON;
        /// </summary>
        [System.Serializable]
        public class ListOfNodes
        {
          public  List<Node>  Row= new List<Node>();
        }
    }
}