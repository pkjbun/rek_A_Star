using AStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit 
{

    #region Custom Methods
    public void Move(Stack<Node> nodes);
    public void StopMoving();
    /// <summary>
    /// Used to Set Unit Stats
    /// </summary>
    /// <param name="unitStats">Stats of Uni</param>
    public void SetUnitStats(UnitStats unitStats);
    /// <summary>
    /// Returns Stats of Unit
    /// </summary>
    /// <returns>Stats of Unit</returns>
    public UnitStats GetUnitStats();
    public void ReceiveNotification();
    public void ReceiveNotification(IUnit unit);
    #endregion
}
