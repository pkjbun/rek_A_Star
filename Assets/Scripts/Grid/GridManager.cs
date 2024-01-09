using System.Collections.Generic;
using UnityEngine;
using Utils;


namespace AStar
{
    public class GridManager : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] private GridSerializeData gridSerializeData;
        [SerializeField] private List<List<Node>> grid = new List<List<Node>>();
        [SerializeField] private GameObject nodePrefab;
        #endregion
        #region Custom Methods
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
                return grid[y][x];
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
    }
}