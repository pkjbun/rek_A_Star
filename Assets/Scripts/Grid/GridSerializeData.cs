using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    [System.Serializable]
    public class GridSerializeData
    {
        #region Fields And Variables
        public int Width;
        public int Height;
        public List<ListNodeData> Data=new List<ListNodeData>();
        public void GenerateData()
        {
             Data = new List<ListNodeData>();

            for (int y = 0; y < Height; y++)
            {
                ListNodeData row = new ListNodeData();

                for (int x = 0; x < Width; x++)
                {
                    // Create a new NodeData instance. 
                    NodeData newNode = new NodeData(new Vector3(x,0,y), x, y, 1, true); // Assuming all nodes are walkable by default
                    row.Row.Add(newNode);
                }

                Data.Add(row);
            }

        }
        /// <summary>
        /// Class to help serialize both in editor and in JSON;
        /// </summary>
        [System.Serializable]
        public class ListNodeData
        {
            public List<NodeData> Row=new List<NodeData>();
        }
        #endregion
    }
}