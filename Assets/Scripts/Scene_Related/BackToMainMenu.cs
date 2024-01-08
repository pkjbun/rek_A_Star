using UnityEngine;
using UnityEngine.UI;

namespace Scenes
{
    public class BackToMainMenu : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] private Button backToMainMenuButton;
        #endregion
        #region Unity Methods
        private void OnEnable()
        {
            backToMainMenuButton?.onClick.AddListener(()=>LoadMainMenuScene());
        }
        private void OnDisable()
        {
            backToMainMenuButton?.onClick.RemoveListener(() => LoadMainMenuScene());
        }
        #endregion
        #region Custom Methods
        public void LoadMainMenuScene()
        {
            Scenes.SceneManager.GetSceneManager()?.LoadMainMenuScene();
        }
        #endregion
    }
}
