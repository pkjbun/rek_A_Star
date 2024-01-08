using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields And Variables
    [SerializeField] private int numberOfUnits=3;
    private static GameManager instance;
    #endregion
   
    #region Unity Methods
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    #region Custom Methods
    /// <summary>
    /// Method to get instance of GameManager
    /// </summary>
    /// <returns>instance of Game manager</returns>
    public static GameManager GetInstance()
    {
        return instance;
    }
    /// <summary>
    /// Used to set number of Units
    /// </summary>
    /// <param name="nr">number of Units to set, this number will be used to spawne units</param>
    public void SetNumberOfUnits(int nr)
    {
        numberOfUnits = nr;
    }
    /// <summary>
    /// Methods to get number of Units that should be spawned in gameScene
    /// </summary>
    /// <returns>Number of Units that should be spawned in game scene</returns>
    public int GetNumberOfUnits() { return numberOfUnits; }
    #endregion

}
