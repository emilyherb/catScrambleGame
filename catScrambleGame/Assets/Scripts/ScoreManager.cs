using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0;
    public Text liveScoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentScore = 0;
        UpdateLiveScore();
    }

    public void AddPoints(int amount)
    {
        currentScore += amount;
        UpdateLiveScore();
    }

    void UpdateLiveScore()
    {
        if (liveScoreText != null)
            liveScoreText.text = "Score: " + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateLiveScore();
    }
}

