using UnityEngine;
using UnityEngine.UI;

public class ObstaclePlacementManager : MonoBehaviour
{
    #region Fields And Variables
   [SerializeField] private GameObject obstaclePrefab; // Reference to the obstacle prefab
    [SerializeField] private LayerMask nodeLayerMask;   // LayerMask for grid detection
    [SerializeField] private Toggle toggleObstacle;
    [SerializeField] private LayerMask terrainLayerMask;
    [SerializeField] private bool isPlacingObstacle = false;
    [SerializeField] private Camera mainCamera;
  
    #endregion
    #region Unity Methods
    void Start()
    {
        mainCamera = Camera.main;
        toggleObstacle?.onValueChanged.AddListener(SetObstaclePlacement);
    }

    void Update()
    {
        if (isPlacingObstacle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObstacle();
            }
        }
    }
    #endregion
    #region Custom Methods
    void ToggleObstaclePlacement()
    {
        isPlacingObstacle = !isPlacingObstacle;
    }
    void SetObstaclePlacement(bool On)
    {
        isPlacingObstacle = On;
    }
    void PlaceObstacle()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, nodeLayerMask))
        {
            if (((1 << hit.collider.gameObject.layer) & terrainLayerMask) == 0)
            {
                Vector3 spawnPosition = hit.point + new Vector3(0, 0.2f, 0);

                Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
    #endregion

}
