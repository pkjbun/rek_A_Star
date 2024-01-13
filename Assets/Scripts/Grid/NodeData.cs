using UnityEngine;

namespace AStar
{[System.Serializable]
    public class NodeData
    {
        #region Fields And Variables
        public Vector3 WorldPosition;
        public int GridX, GridY;
        public float Cost;
        public bool IsWalkable;
        #endregion
        
        public NodeData(Vector3 worldPosition, int gridX, int gridY, float cost, bool isWalkable)
        {
        WorldPosition = worldPosition;
        GridX = gridX;
        GridY = gridY;
        Cost = cost;
        IsWalkable = isWalkable;
        }
    }
    
}
