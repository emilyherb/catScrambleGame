using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject endScreenPanel;
    public Text finalScoreText;

    public string mainMenuSceneName = "StartMenu"; // Name of the StartMenu scene
    public string gameSceneName = "Tutorial"; // Name of the gameplay scene

    public void ShowEndScreen()
    {
        Time.timeScale = 0f; // Pause the game
        endScreenPanel.SetActive(true); // Show the end screen panel

        // Display the final score if ScoreManager is available
        if (ScoreManager.Instance != null)
        {
            int finalScore = ScoreManager.Instance.currentScore;
            if (finalScoreText != null)
            {
                finalScoreText.text = "Score: " + finalScore;
            }
            else
            {
                Debug.LogWarning("finalScoreText is not assigned in GameOverManager!");
            }
        }
        else
        {
            Debug.LogError("ScoreManager.Instance is null! Cannot display final score.");
            if (finalScoreText != null)
            {
                finalScoreText.text = "Score: N/A";
            }
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f; // Resume game time
        endScreenPanel.SetActive(false); // Hide the end screen
        ScoreManager.Instance?.ResetScore(); // Reset the score

        // Reset the player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.SetActive(true);
            player.transform.position = new Vector3(9.4f, -3.54f, -44.8f); // Reset position
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Reset velocity
            }

            // Reset health
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.ResetHealth();
            }
        }

        // Reset GameLoop to start scoring again
        GameLoop gameLoop = FindObjectOfType<GameLoop>();
        if (gameLoop != null)
        {
            gameLoop.Reset();
        }

        // Reload the Tutorial scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f; // Ensure game is paused
        endScreenPanel.SetActive(false); // Hide the end screen
        ScoreManager.Instance?.ResetScore(); // Reset the score

        SceneManager.LoadScene(mainMenuSceneName);
    }
}