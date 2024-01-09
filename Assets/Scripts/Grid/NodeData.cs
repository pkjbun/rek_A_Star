using UnityEngine;

namespace AStar
{[System.Serializable]
    public class NodeData
    {
        #region Fields And Variables
        public Vector2 WorldPosition;
        public int GridX, GridY;
        public float Cost;
        public bool IsWalkable;
        #endregion
    }
}
