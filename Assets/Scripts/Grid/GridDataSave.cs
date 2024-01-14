using AStar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Threading.Tasks;

public class GridDataSave : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] GridManager gridManager;
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
    /// <summary>
    /// Use to save Grid Data
    /// </summary>
    [ContextMenu("Save Grid data to file")]
    public async void WriteData()
    {
        await SaveGridDataAsync();
    }
    public async void RestoreGridData()
    {
        await LoadGridDataAsync();
    }    
    private async Task SaveGridDataAsync()
    {
        gridManager.UpdateGridSerializeData();
        string path = Path.Combine(Application.dataPath, "../Data/GridData.json");
        string jsonData = JsonUtility.ToJson(gridManager.GetGridData());
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            await writer.WriteAsync(jsonData);
        }
    }

    // Asynchronous method to load gridData from a JSON file
    public async Task LoadGridDataAsync()
    {
        string path = Path.Combine(Application.dataPath, "../Data/GridData.json");
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string jsonData = await reader.ReadToEndAsync();
                gridManager.RestoreGrid(JsonUtility.FromJson<GridSerializeData>(jsonData));
            }
        }
        else
        {
            Debug.LogError("File not found.");
        }
    }
    #endregion
}
