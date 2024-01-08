using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadUnitStats : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] private List<UnitStats> listOfUnitStats=new List<UnitStats>(); 
    #endregion
#region Unity Methods
#endregion
#region Custom Methods
    /// <summary>
    /// To save Unit Stats to file. It uses Helper class to serilize list
    /// </summary>
    public void SaveUnitStats()
    {
        //TODO: Get Unit Stats from Unit manager and fill list;
        string json = JsonUtility.ToJson(new Serialization<UnitStats>(listOfUnitStats));
        string path = Path.Combine(Application.dataPath, "../Data/unitStats.json");
        File.WriteAllText(path, json);
        Debug.Log("Zapisano listê UnitStats do " + path);
    }
    public void LoadUnitsStat()
    {
        string path = Path.Combine(Application.dataPath, "../Data/unitStats.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            listOfUnitStats = JsonUtility.FromJson<Serialization<UnitStats>>(json).ToList();
            Debug.Log("Wczytano listê UnitStats z " + path);
            UpdateUnitStats();
        }
        else
        {
            Debug.LogError("Nie znaleziono pliku: " + path);
        }
    }

    private void UpdateUnitStats()
    {
        //TODO: after UnitManager is ready make method to restore unit data
    }
    #endregion
}
