using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0;
    public Text liveScoreText;  // This will be assigned automatically if not manually set

    private bool hasReturnedToMenu = false;

    void Awake()
    {
        // If no instance exists, set it up as the singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy on scene change
        }
        else
        {
            Destroy(gameObject); // Destroy if already exists
        }

        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Initialize the score
        currentScore = 0;
        UpdateLiveScore();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset score when returning to StartMenu
        if (scene.name == "StartMenu")
        {
            ResetScore();
            hasReturnedToMenu = false; // Reset flag for future runs
        }

        // Always try to find the "ScoreText" object in the new scene
        liveScoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();

        if (liveScoreText == null)
        {
            Debug.LogError("No 'ScoreText' object found in the new scene!");
        }
        else
        {
            UpdateLiveScore(); // Update the score display after scene loads
        }
    }

    public void AddPoints(int amount)
    {
        currentScore += amount;
        UpdateLiveScore();

        // Check if score reaches 20, and return to the Start Menu
        if (currentScore >= 20 && !hasReturnedToMenu)
        {
            hasReturnedToMenu = true;
            ReturnToMainMenu();
        }
    }

    void UpdateLiveScore()
    {
        if (liveScoreText != null)
            liveScoreText.text = "Score: " + currentScore;
        else
            Debug.LogWarning("liveScoreText is not assigned in ScoreManager!");
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateLiveScore();
    }

    private void ReturnToMainMenu()
    {
        Debug.Log("Reached 20 points! Returning to Start Menu...");
        SceneManager.LoadScene("StartMenu");
    }

    // Clean up sceneLoaded listener when the object is destroyed
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
