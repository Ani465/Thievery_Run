using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ApplicationManager : MonoBehaviour
    {
        public GameObject settingPanel;

        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
        
        public void OpenSettingPanel()
        {
            settingPanel.SetActive(true);
        }

        public void CloseSettingPanel()
        {
            settingPanel.SetActive(false);
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
}