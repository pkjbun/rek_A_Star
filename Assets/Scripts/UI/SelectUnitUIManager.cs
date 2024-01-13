using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AStar.UI {
    public class SelectUnitUIManager : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private RectTransform buttonParent;
        private Dictionary<Button,int > buttonDictionary = new Dictionary< Button, int>();
        
        #endregion
        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            GenerateUnitButtons();
        }

  

        // Update is called once per frame
        void Update()
        {

        }
        #endregion
        #region Custom Methods
        /// <summary>
        /// Method used to find button in dictionary and select Unit with matching nr
        /// </summary>
        /// <param name="buttonInst">instance of button</param>
        private void SelectUnitNr(Button buttonInst)
        {
            buttonDictionary.TryGetValue(buttonInst, out int nr);
            UnitManager.GetInstance()?.SetCurrentLeadingUnit(nr);
        }
        /// <summary>
        /// Public method to select Unit with nr
        /// </summary>
        /// <param name="nr">number of unit</param>
        public void SelectUnitNr(int nr)
        {
            UnitManager.GetInstance()?.SetCurrentLeadingUnit(nr);
        }
        /// <summary>
        /// Method for generating unit
        /// </summary>
        private void GenerateUnitButtons()
        {
            if (buttonPrefab != null && buttonParent != null)
            {
                List<UnitBase> ub = UnitManager.GetInstance()?.GetListOfUnit();
                for (int i = 0; i < ub.Count; i++)
                {
                    Button buttonInst = Instantiate(buttonPrefab, buttonParent);
                    buttonDictionary.Add(buttonInst, i);
                    buttonInst.onClick.AddListener(() => SelectUnitNr(buttonInst));
                }
            }
        }
        #endregion
    }
}