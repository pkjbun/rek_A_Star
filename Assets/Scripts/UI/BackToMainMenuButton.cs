using Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AStar.UI
{
    public class BackToMainMenuButton : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] private Button backToMainMenuButton;

        #endregion
        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            HandleBackToMainButton();
        }

        #endregion
        #region Custom Methods
        /// <summary>
        /// Use to Handle clicking on Back to Main Menu Button
        /// </summary>
        private void HandleBackToMainButton()
        {
            if (backToMainMenuButton != null)
            {
                backToMainMenuButton.onClick.AddListener(() => BackToMainMenu());
            }
        }
        /// <summary>
        /// Returns to Main Menu
        /// </summary>
        public void BackToMainMenu()
        {
            SceneManager.GetSceneManager().LoadMainMenuScene();
        }
        #endregion
    }
}