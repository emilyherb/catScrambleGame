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

            // Check for score thresholds based on the scene name
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "Tutorial" && currentScore >= 20)
            {
                ReturnToMainMenu();
            }
            else if (sceneName == "Level 1" && currentScore >= 30)
            {
                ReturnToMainMenu();
            }
            else if (sceneName == "Level 2" && currentScore >= 40)
            {
                ReturnToMainMenu();
            }
            else if (sceneName == "Level 3" && currentScore >= 50)
            {
                ReturnToMainMenu();
            }
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

    // Return to the main menu based on the current scene
    public void ReturnToMainMenu()
    {
        Debug.Log("Score reached threshold, returning to Start Menu...");

        // Load the StartMenu scene
        SceneManager.LoadScene("StartMenu");
    }
}
