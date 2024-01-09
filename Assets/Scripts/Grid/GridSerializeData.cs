using System.Collections.Generic;
namespace AStar
{
    [System.Serializable]
    public class GridSerializeData
    {
        #region Fields And Variables
        public int Width;
        public int Height;
        public List<NodeData> Data=new List<NodeData>();
        #endregion
    }
}