using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AStar.Inputs
{
    public class InputManager : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] Camera mainCamera;
        [SerializeField] LayerMask layerMask;
        [SerializeField] UnitManager unitManager;
        #endregion
        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            layerMask = GridManager.GetInstance().GetLayerMask();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DetectNode();
            }
        }
        /// <summary>
        /// Detects Node and Sends this Node to UnitsManager
        /// </summary>
        void DetectNode()
        {
            // Convert the click/touch position to a ray
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,1000,layerMask,QueryTriggerInteraction.Collide))
            {
                // Check if the object hit is a Node
                Node node = hit.collider.GetComponentInParent<Node>();
                if (node != null)
                {
                    // Node has been clicked
                    Debug.Log("Clicked Node: " + node.name);
                    if (unitManager == null) { unitManager = UnitManager.GetInstance(); }
                    unitManager?.HandleInput(node);
                }
            }
        }
        #endregion
        #region Custom Methods

        #endregion
    }
}