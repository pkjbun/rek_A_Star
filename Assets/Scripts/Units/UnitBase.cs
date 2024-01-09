using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour, IUnit
{
    #region Fields And Variables
    [SerializeField] private UnitStats unitStats=new UnitStats();
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

        public void MoveTo()
        {
         //   throw new System.NotImplementedException();
        }

        public void SetUnitStats(UnitStats Stats)
        {
        if (Stats != null) { unitStats = Stats; }
       
        }

        public void StopMoving()
        {
         //  throw new System.NotImplementedException();
        }
    /// <summary>
    
    /// </summary>
    /// <param name="Stats"></param>
    public void UnitSetup()
    {
        unitStats.GenerateRandomStats();
    }
    #endregion
}
