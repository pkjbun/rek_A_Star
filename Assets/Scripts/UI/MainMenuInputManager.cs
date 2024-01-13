using Scenes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace AStar.UI
{
    public class MainMenuInputManager : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] private int minNumberOfUnits = 3;
        [SerializeField] private int maxNumberOfUnits = 10;
        [SerializeField] private int currentNumberOfUnits = 3;
        [SerializeField] private TextMeshProUGUI numberOfUnitsIndicator;
        [SerializeField] private Button increaseNumberOfUnitsButton;
        [SerializeField] private Button decreaseNumberOfUnitsButton;
        [SerializeField] private Button appQuitButton;
        [SerializeField] private Button loadSceneButton;
        #endregion
        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            LoadSceneQuitButtons();
            HandleNumberButtons();

        }

     
        // Update is called once per frame
        void Update()
        {

        }
        #endregion
        #region Custom Methods
        /// <summary>
        /// Use to Handle clicking on LoadSceneButton
        /// </summary>
        public void LoadSceneButtonHandle()
        {
            SceneManager.GetSceneManager()?.LoadGameScene();
        }
        /// <summary>
        /// Quits application or closes editor
        /// </summary>
        public void AppQuit()
        {   
            #if UNITY_STANDALONE
            Application.Quit();
            #endif
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }  
        /// <summary>
        /// Add listerners to buttons that increase/decrease number of units
        /// </summary>
        private void HandleNumberButtons()
        {
            if (increaseNumberOfUnitsButton != null)
            {
                increaseNumberOfUnitsButton.onClick.AddListener(() => IncreaseNumber());
            }
            if (decreaseNumberOfUnitsButton != null)
            {
                decreaseNumberOfUnitsButton.onClick.AddListener(() => DecreaseNumber());
            }
        }
        /// <summary>
        /// Decreases number of Units, checks if the number is withing bounds
        /// </summary>
        private void DecreaseNumber()
        {
            currentNumberOfUnits--;
            if (currentNumberOfUnits < minNumberOfUnits)
            {
                currentNumberOfUnits = minNumberOfUnits;
            }
            UpdateNumberOfUnits();
        }
        /// <summary>
        /// Increases number of Units, checks if the number is withing bounds
        /// </summary>
        private void IncreaseNumber()
        {
            currentNumberOfUnits++;
            if(currentNumberOfUnits > maxNumberOfUnits)
            {
                currentNumberOfUnits = maxNumberOfUnits;
            }
            UpdateNumberOfUnits();
        }
        /// <summary>
        /// Updates number of units in UI and GameManger
        /// </summary>
        private void UpdateNumberOfUnits()
        {   if (numberOfUnitsIndicator)
            { numberOfUnitsIndicator.text = currentNumberOfUnits.ToString(); }
            GameManager.GetInstance().SetNumberOfUnits(currentNumberOfUnits);
        }
        /// <summary>
        /// Add listerners to buttons that Quits App or loads game scene
        /// </summary>
        private void LoadSceneQuitButtons()
        {
            if (appQuitButton != null)
            {
                appQuitButton.onClick.AddListener(() => AppQuit());
            }
            if (loadSceneButton != null)
            {
                loadSceneButton.onClick.AddListener(() => LoadSceneButtonHandle());
            }
        }

        #endregion
    }
}