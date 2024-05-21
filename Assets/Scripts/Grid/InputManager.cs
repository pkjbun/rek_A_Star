using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AStar.Inputs
{
    public class InputManager : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] Camera mainCamera;
        [SerializeField] LayerMask layerMask;
        [SerializeField] UnitManager unitManager;
        [SerializeField] bool DetectClick=true;
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
            if (IsPointerOverUIElement())
            {
                return;
            }
            if (Input.GetMouseButtonDown(0) && DetectClick)
            {
                DetectNode();
            }
        }
       
        #endregion
        #region Custom Methods
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
        /// <summary>
        /// used to stop detecting when placing obstacle
        /// </summary>
        public void ToggleDetecting()
        {
            DetectClick = !DetectClick;
        }
        /// <summary>
        /// Use to disable input when mouse is over UI
        /// </summary>
        /// <returns></returns>
        private bool IsPointerOverUIElement()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
        #endregion
    }
}