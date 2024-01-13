using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] private UnitBase unitPrefab;
    [SerializeField] private List<UnitBase> listOfUnits=new List<UnitBase>();
    [SerializeField] private UnitBase currentLeadingUnit;
    [SerializeField] private Transform spawnPoint;
    private static UnitManager instance;


    #endregion
    #region Unity Methods
    private void Awake()
    {
        instance = this;
    }
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
    public void SpawnUnits()
    {
        int numberOfUnits = GameManager.GetInstance().GetNumberOfUnits();
        float spacing = 0.7f;

        for (int i = 0; i < numberOfUnits; i++)
        {
            Vector3 spawnPosition = CalculatePosition(spacing, i);

            UnitStats us = new UnitStats();
            UnitBase newUnit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
            newUnit.SetUnitStats(us);
             newUnit.UnitSetup();
            listOfUnits.Add(newUnit);
        }
    }
    /// <summary>
    /// Simple placeholder method used for calculating position of units in scene during 
    /// </summary>
    /// <param name="spacing">Space bettween Units</param>
    /// <param name="i">unit number</param>
    /// <returns></returns>
    private Vector3 CalculatePosition(float spacing, int i)
    {
     
        return new Vector3(i * spacing, 0, 0) + (spawnPoint? spawnPoint.position : Vector3.zero);
    }
    /// <summary>
    /// To Get Current Leading Unit
    /// </summary>
    /// <returns>current leading unit</returns>
    public UnitBase GetCurrentLeadingUnit()
    {
        return currentLeadingUnit;
    }
    /// <summary>
    /// sets leading unit number
    /// </summary>
    /// <param name="nr">number of unit(in list), that should be set as leading</param>
    public void SetCurrentLeadingUnit(int nr)
    {
        if(nr<listOfUnits.Count)
        {
            currentLeadingUnit = listOfUnits[nr];
        }
    }
    public List<UnitBase> GetListOfUnit() { return listOfUnits; }
    public void HandleMoveUnits()
    {
        //TODO:write it...
    }  
    public static UnitManager GetInstance() {return instance;}
    #endregion
}