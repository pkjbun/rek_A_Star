using UnityEditor;
using UnityEngine;
namespace Scenes
{
    public class SceneManager : MonoBehaviour
    {
        #region Fields And Variables
        private static SceneManager instance;
        #endregion
        #region Unity Methods
        void Awake()
        {
            if (instance == null)
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
        public static SceneManager GetSceneManager() { return instance; }
        /// <summary>
        /// Use to Load Game Scene. Right now this is hardoced but I know how to make it more flexible
        /// </summary>
        public void  LoadGameScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        /// <summary>
        /// Use to Load MainMenuScene
        /// </summary>
        public void LoadMainMenuScene() { UnityEngine.SceneManagement.SceneManager.LoadScene(0); }
        #endregion
    }
}
