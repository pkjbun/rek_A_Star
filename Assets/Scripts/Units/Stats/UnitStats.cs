
using System;
using Random = UnityEngine.Random;
[Serializable]
public class UnitStats 
{
    #region Fields And Variables
    public float Speed;
    public float Maneuverability;
    public float Durablibity;


    #endregion

    #region Custom Methods
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="speed"> speed of unity</param>
    /// <param name="maneuverability">maneuverability of unit</param>
    /// <param name="durability"> durability of unit</param>
   
    public UnitStats(float speed, float maneuverability, float durability)
    {   
        this.Speed = speed;
        this.Durablibity = durability;
        this.Maneuverability = maneuverability;
    }
    public UnitStats() { }
    public void GenerateRandomStats()
    {
        Speed = Random.Range(0, 100f);
        Maneuverability = Random.Range(0, 40f);
        Durablibity = Random.Range(0, 60f);
    }
    #endregion
}
