using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Settings")]
    [SerializeField] private string scorePrefixString = "Score: ";

    private int currentScore;

    public int CurrentScore => currentScore;

    public void AddScore(int value)
    {
        currentScore += value;
        SetScoreText();
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


}
