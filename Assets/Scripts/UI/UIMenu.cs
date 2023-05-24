using UnityEngine;
using UnityEngine.SceneManagement;

    public class UIMenu : MonoBehaviour
    {
        private readonly string gameScene = "GameScene";

        public void StartGame()
        {
            SceneManager.LoadScene(gameScene);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
