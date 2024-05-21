using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace AStar
{
    public class Node : MonoBehaviour
    {
        #region Fields And Variables
        public Node Parent;
        [SerializeField] private List<Node> adjacencies = new List<Node>();
        [SerializeField] private NodeData nodeData;
        [SerializeField] private GridManager gridManager;
        [SerializeField] private LayerMask obstacleLayerMask;
        public float CurrrentCost;
        public float DistanceToTarget;

        public NodeData NodeData { get => nodeData; set => nodeData = value; }
        #endregion
        #region Unity Methods
        
        void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & obstacleLayerMask) != 0)
            {
                nodeData.IsWalkable = false;
                Debug.Log($"Node {name} is now not walkable due to obstacle.");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & obstacleLayerMask) != 0)
            {
                nodeData.IsWalkable = true;
                Debug.Log($"Node {name} is now walkable again as obstacle is removed.");
            }
        }
        #endregion
        #region Custom Methods
        public Node() { }
        public Node(NodeData data)
        {
            nodeData = data;
        }
        /// <summary>
        /// Setups Node, sets refrence to Grid Manager
        /// </summary>
        /// <param name="GridManager">refrenece to Grid Manager</param>
        public void Setup(GridManager GridManager, NodeData data)
        {
            nodeData = data;
            gridManager = GridManager;
        }
        public List<Node> GetAdjacencies()
        {
            if (adjacencies.Count == 0) { FindAdjacencies(); }
            return adjacencies;
        }
        /// <summary>
        /// Internal function to find adjancencies of Node
        /// </summary>
        private void FindAdjacencies()
        {
            adjacencies = new List<Node>();

            adjacencies.Clear();
            /// Commented out, because Recruiter wanted only 4 directions
            /* int[] dx = { 0, 1, 0, -1, -1,-1,1,1 }; // Directions for left, down, right, up, left down, left up, right down, right up
             int[] dy = { -1, 0, 1, 0, -1, 1,-1,1 };
             */
            int[] dx = { 0, 1, 0, -1 }; // Directions for left, down, right, up
            int[] dy = { -1, 0, 1, 0 };
            ///
            for (int i = 0; i < dx.Count(); i++)
            {
                int adjX = nodeData.GridX + dx[i];
                int adjY = nodeData.GridY + dy[i];

                if (IsValidGridPosition(adjX, adjY))
                {
                    Node adjacentNode = gridManager.GetNodeAt(adjX, adjY);
                    if (adjacentNode != null)
                    {
                        adjacencies.Add(adjacentNode);
                    }
                }
            }
        }
        /// <summary>
        /// Internal function to check if Node is within Grid bounds
        /// </summary>
        /// <param name="x">x coordinate of node in Grid Layout</param>
        /// <param name="y">y coordinate of node in Grid Layout</param>
        /// <returns></returns>
        private bool IsValidGridPosition(int x, int y)
        {
          return x >= 0 && y >= 0 && x < gridManager.GetGridData().Width && y < gridManager.GetGridData().Height;
        }
        public bool IsWalkable()
        {
            return nodeData.IsWalkable;
        }
        public float CalculateMoveF()
        {
            if (DistanceToTarget != -1 && CurrrentCost != -1)
                return DistanceToTarget + CurrrentCost;
            else return -1;
        }
        public float GetMoveCost()
        {
            return nodeData.Cost;
        }
        public Vector2 GetWorldPosition()
        {
          return  new Vector2 (nodeData.WorldPosition.x, nodeData.WorldPosition.z);
        }
        public NodeData GetNodeData() { return nodeData; }
        #endregion
    }
}