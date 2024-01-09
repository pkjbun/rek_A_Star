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
    {   if(UnitManager.GetInstance()==null) {
            Debug.Log("Missing Unit Manager");
            return; }
        listOfUnitStats.Clear(); //CLEAR list to ensure that there are no old stats
        foreach (UnitBase unit in UnitManager.GetInstance().GetListOfUnit()) {
            listOfUnitStats.Add(unit.GetUnitStats());
        }
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
    /// <summary>
    /// Method Used to restore unit stats basing on list from file
    /// </summary>
    private void UpdateUnitStats()
    {
        if (UnitManager.GetInstance() == null)
        {
            Debug.Log("Missing Unit Manager, can't restore stats");
            return;
        }
        List<UnitBase> LUnitBase = UnitManager.GetInstance().GetListOfUnit();
        int counter=0;
        if(LUnitBase.Count>= listOfUnitStats.Count)
        {
            counter = listOfUnitStats.Count;
        }
        else
        {
            counter = LUnitBase.Count;
          
        }  
        for (int i = 0; i < counter; i++)
            {
                LUnitBase[i].SetUnitStats(listOfUnitStats[i]);
            }
    }
    #endregion
}
