using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0;
    public Text liveScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentScore = 0;
        UpdateLiveScore();
    }

    void Start()
    {
        liveScoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();

        if (liveScoreText != null)
        {
            UpdateLiveScore();
        }
        else
        {
            Debug.LogWarning("ScoreText not found in the current scene.");
        }
    }

    public void AddPoints(int amount)
    {
        if (currentScore >= 0)
        {
            currentScore += amount;
            UpdateLiveScore();

            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "Tutorial" && currentScore >= 50)
            {
                ReturnToMainMenu();
            }
            else if (sceneName == "Level 1" && currentScore >= 100)
            {
                ReturnToMainMenu();
            }
            else if (sceneName == "Level 2" && currentScore >= 200)
            {
                ReturnToMainMenu();
            }
            else if (sceneName == "Level 3" && currentScore >= 300)
            {
                ReturnToMainMenu();
            }
        }
    }

    void UpdateLiveScore()
    {
        if (liveScoreText != null)
        {
            liveScoreText.text = "Score: " + currentScore;
        }
        else
        {
            Debug.LogWarning("liveScoreText is not assigned.");
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateLiveScore();
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Score reached threshold, returning to Start Menu...");

        SceneManager.LoadScene("StartMenu");
    }
}
