using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameUIManager : MonoBehaviour
    {
        public GameObject gameOverUI;
        public TMP_Text scoreText;
        public GameObject resumeButton;
        public GameObject objectSpawner;
    
        public void GameOver()
        {
            PlayerPrefs.SetInt("score", ScoreManager.Instance.GetScore());
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
            scoreText.text = "Score: " + PlayerPrefs.GetInt("score");
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ScoreManager.Instance.ResetScore();
        }
        
        public void ResumeGame()
        {
            Time.timeScale = 1;
            resumeButton.SetActive(false);
            objectSpawner.SetActive(true);
        }
        
        public void Quit()
        {
            SceneManager.LoadScene(0);
        }
    }
}