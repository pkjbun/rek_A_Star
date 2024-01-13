using AStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour, IUnit
{
    #region Fields And Variables
    [SerializeField] private UnitStats unitStats=new UnitStats();
    [SerializeField] private Node currentNode;
    [SerializeField] private Collider[] coll=new Collider[1];
    private Stack<Node> stack = new Stack<Node>();
    #endregion
  
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Custom Methods
      public UnitStats GetUnitStats()
        {
           return unitStats;
        }

        public void Move(Stack<Node> nodes)
        {
         //   throw new System.NotImplementedException();
        }
        /// <summary>
        /// Sets Unit Stats
        /// </summary>
        /// <param name="Stats"> Stats to set</param>
        public void SetUnitStats(UnitStats Stats)
        {
        if (Stats != null) { unitStats = Stats; }
       
        }

        public void StopMoving()
        {
         //  throw new System.NotImplementedException();
        }
        /// <summary>
        /// Generates random Unit Stats in case of more complex game it would  be diffrent 
        /// </summary>
        public void UnitSetup()
        {
            unitStats.GenerateRandomStats();
        }
        public void DetectCurrentNode()
        {
            coll[0] = null; 
            Physics.OverlapSphereNonAlloc(transform.position, 0.3f, coll, GridManager.GetInstance().GetLayerMask(),QueryTriggerInteraction.Collide);
            if (coll[0] != null)
            {
               Node n= coll[0].GetComponentInParent<Node>();
            if(n != null) { currentNode = n; }
            }
        }
        public Node GetCurrentNode() { return currentNode; }
    #endregion
}
