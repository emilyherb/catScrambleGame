using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0;
    public Text liveScoreText; // This will be assigned dynamically

    void Awake()
    {
        // Make sure only one ScoreManager exists in the scene at a time
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy if another instance already exists
        }

        // Initialize the score when the scene is loaded
        currentScore = 0;
        UpdateLiveScore();
    }

    void Start()
    {
        // When the scene is loaded, check for the ScoreText and assign it
        liveScoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();

        if (liveScoreText != null)
        {
            UpdateLiveScore(); // Update the score display
        }
        else
        {
            Debug.LogWarning("ScoreText not found in the current scene.");
        }
    }

    // Method to add points to the score
    public void AddPoints(int amount)
    {
        if (currentScore >= 0)
        {
            currentScore += amount;
            UpdateLiveScore();
        }
    }

    // Method to update the live score text
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

    // Method to reset the score
    public void ResetScore()
    {
        currentScore = 0;
        UpdateLiveScore();
    }

    // If needed, return to the main menu
    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Start Menu...");
        SceneManager.LoadScene("StartMenu");
    }
}
