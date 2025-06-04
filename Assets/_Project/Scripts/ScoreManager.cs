using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Settings")]
    [SerializeField] private string scorePrefixString = "Score: ";
    [SerializeField] private string highScorePrefixString = "Highscore: ";

    private int currentScore;
    private int highScore;

    public int CurrentScore => currentScore;
    public int HighScore => highScore;

    public void AddScore(int value)
    {
        currentScore += value;
        SetScoreText();
        if (currentScore > highScore) { 
            highScore = currentScore;
            SetHighscoreText();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = $"{scorePrefixString}{currentScore}";
    }

    private void SetHighscoreText()
    {
        highScoreText.text = $"{highScorePrefixString}{highScore}";
    }


}
