using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class UnitManager : MonoBehaviour
{
    #region Fields And Variables
    private UnitBase unitPrefab;
    [SerializeField] private List<UnitBase> listOfUnits=new List<UnitBase>();
    [SerializeField] private UnitBase currentLeadingUnit;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spacing;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private AssetReference unitReference;
    private static UnitManager instance;
    public event Action OnAllUnitsSpawned;
    GameObject go;
    List<UnitBase> otherUnits;
    #endregion
    #region Unity Methods
    private void Awake()
    {
        instance = this;
        LoadPrefab();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {   //I know that it will be released after unloading scene...
        Addressables.Release(go);
    }
    #endregion
    #region Custom Methods
   
     void LoadPrefab()
    {
        
        unitReference.InstantiateAsync().Completed += OnPrefabLoaded;
        
    }

    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            go = handle.Result;
            UnitBase ub= go.GetComponent<UnitBase>();
            unitPrefab = ub;
            go.SetActive(false);
           SpawnUnits();
            SetCurrentLeadingUnit(0);
        }
        else
        {
            Debug.Log("Error loading UnitBase Adressable");
        }
    }

    /// <summary>
    /// Method for sapwning Units at start
    /// </summary>
    private void SpawnUnits()
    {   if(GameManager.GetInstance()==null) { Debug.LogWarning("No Game Manager"); return; }
        int numberOfUnits = GameManager.GetInstance().GetNumberOfUnits();

        for (int i = 0; i < numberOfUnits; i++)
        {
            Vector3 spawnPosition = CalculatePosition(spacing, i);
            UnitBase newUnit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
            AfterSpawnSet(newUnit);
        }
        OnAllUnitsSpawned?.Invoke();
    }
    /// <summary>
    /// To Set Units after spawning
    /// </summary>
    /// <param name="newUnit"></param>
    private void AfterSpawnSet(UnitBase newUnit)
    {   newUnit.gameObject.SetActive(true);
        UnitStats us = new UnitStats();
        newUnit.SetUnitStats(us);
        newUnit.UnitSetup();
        newUnit.DetectCurrentNode();
        listOfUnits.Add(newUnit);
    }

    /// <summary>
    /// Simple placeholder method used for calculating position of units in scene during
    /// spawn phase
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
        if (currentLeadingUnit != null)
        {
            // Unsubscribe all units from the old leading unit's event
            currentLeadingUnit.OnTaskCompleted -= OnLeadingUnitTaskCompleted;
        }

        if (nr<listOfUnits.Count)
        {   
            currentLeadingUnit = listOfUnits[nr];
        }
        if (currentLeadingUnit != null)
        {
            // Subscribe all units to the new leading unit's event
            currentLeadingUnit.OnTaskCompleted += OnLeadingUnitTaskCompleted;
        }
        ///just to ensure that there is no missing references
        if (listOfUnits[nr] == null) { listOfUnits.RemoveAll(x => x = null); };
    }

    private void OnLeadingUnitTaskCompleted(UnitBase unit)
    {
        foreach (var otherUnit in listOfUnits)
        {
            if (otherUnit != currentLeadingUnit)
            {
                otherUnit.ReceiveNotification();
            }
        }
    }

    public List<UnitBase> GetListOfUnit() { return listOfUnits; }
    /// <summary>
    /// Move Units Along the path
    /// </summary>
    /// <param name="path"></param>
    private async void HandleMoveUnits(Stack<Node> path)
    {
        Stack<Node> lp = Helper<Node>.CopyStack(path);
        otherUnits = new List<UnitBase>(listOfUnits);
        otherUnits.Remove(currentLeadingUnit);
        currentLeadingUnit.Move(lp);
        await SortUnitsByDistanceToLeaderAsync();
        StartCoroutine(MoveOther(path));
    } 
    IEnumerator MoveOther(Stack<Node> path)
    {
        for(int g= 0; g < otherUnits.Count; g++) 
            { otherUnits[g].StopMoving(); }
        yield return new WaitForSeconds(1.5f);
        for(int i=0; i< otherUnits.Count; i++)
        {
           Stack<Node> lp = Helper<Node>.CopyStack(path);

            otherUnits[i].Move(lp); 
            yield return new WaitForSeconds(0.1f);
        }
    }
    /// <summary>
    /// Sorts Other Units by distance to leader to ensure that they get to move in right order
    /// </summary>
    /// <returns></returns>
    public async Task SortUnitsByDistanceToLeaderAsync()
    {
        if (currentLeadingUnit == null)
        {
            Debug.LogWarning("Current leading unit is not set.");
            return;
        }

        // Capture positions on the main thread
        Vector3 leaderPosition = currentLeadingUnit.transform.position;
        List<(UnitBase unit, Vector3 position)> unitsWithPositions = new List<(UnitBase, Vector3)>();
        foreach (var unit in otherUnits)
        {
            unitsWithPositions.Add((unit, unit.transform.position));
        }

        // Perform sorting on a background thread
        await Task.Run(() =>
        {
            unitsWithPositions.Sort((tuple1, tuple2) =>
            {
                float distance1 = Vector3.Distance(tuple1.position, leaderPosition);
                float distance2 = Vector3.Distance(tuple2.position, leaderPosition);
                return distance1.CompareTo(distance2);
            });
        });

        // Apply sorted order back on the main thread
        for (int i = 0; i < unitsWithPositions.Count; i++)
        {
            otherUnits[i] = unitsWithPositions[i].unit;
        }
    }

    public void HandleInput(Node EndNode)
    {
        if (EndNode == null) { return; }
        if(gridManager == null) { gridManager = GridManager.GetInstance(); }
        if(currentLeadingUnit == null) { Debug.Log("There is no leading unit, will not calculate path");
            return;
        }
   currentLeadingUnit.DetectCurrentNode(); 
       Stack<Node> nodeStack= gridManager?.FindPath(currentLeadingUnit.GetCurrentNode(), EndNode);
       if(nodeStack==null ) {
            Debug.Log("No path, is there no Grid Manager?");
            return; }
        //foreach(Node node in nodeStack)
        //{
        //    Debug.Log("Should walk thorought" + node.name);
        //}
        if(nodeStack.Count > 0)
        {
            HandleMoveUnits(nodeStack);
        }
    }
    public static UnitManager GetInstance() {return instance;}
    #endregion
}
