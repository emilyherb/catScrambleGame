using UnityEngine;
using UnityEngine.SceneManagement;  // Import the SceneManager for loading scenes
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject endScreenPanel;
    public Text finalScoreText;
    public Text highScoreText;

    public GameObject startMenuCanvas;
    public GameObject mainPanel;
    public GameObject scoreCanvas;

    public string gameSceneName = "GameScene";    // Assign this to the name of your game scene
    public string mainMenuSceneName = "MainMenu"; // Assign this to the name of your main menu scene

    public void ShowEndScreen()
    {
        Time.timeScale = 0f;
        endScreenPanel.SetActive(true);
        scoreCanvas.SetActive(false);

        int finalScore = ScoreManager.Instance.currentScore;
        finalScoreText.text = "Score: " + finalScore;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (finalScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            highScore = finalScore;
        }
        highScoreText.text = "High Score: " + highScore;
    }

    public void PlayAgain()
    {
        Debug.Log("🔁 PlayAgain() called");

        Time.timeScale = 1f;  // Resume game time
        endScreenPanel.SetActive(false);
        scoreCanvas.SetActive(true);
        ScoreManager.Instance.ResetScore();

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Reactivate player first
            player.SetActive(true);
            Debug.Log("✅ Reactivated player");

            // Reset health
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.ResetHealth();
                Debug.Log("💖 Reset player health");
            }
            else
            {
                Debug.LogWarning("No PlayerHealth component found!");
            }

            // Reset position & velocity
            player.transform.position = new Vector3(9.4f, -3.54f, -44.8f);
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            Debug.LogError("❌ Could not find player GameObject!");
        }

        // Reload the gameplay scene (GameScene)
        SceneManager.LoadScene(gameSceneName);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f;
        endScreenPanel.SetActive(false);
        startMenuCanvas.SetActive(true);
        mainPanel.SetActive(true);
        scoreCanvas.SetActive(false);
        ScoreManager.Instance.ResetScore();

        // Load the Main Menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
