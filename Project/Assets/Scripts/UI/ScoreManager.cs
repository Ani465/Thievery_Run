using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    private int _score;
    private int _highScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: " + _score.ToString();
        _highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "HighScore: " + _highScore.ToString();
    }

    public void AddScore()
    {
        Debug.Log("AddScore called");
        _score += 5;
        scoreText.text = "Score: " + _score.ToString();
        if (_highScore < _score)
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }
    }
    
    public void ResetScore()
    {
        _score = 0;
    }

    public int GetScore()
    {
        return _score;
    }
}