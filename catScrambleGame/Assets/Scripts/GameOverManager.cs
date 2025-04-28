using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject endScreenPanel;
    public Text finalScoreText;

    public string mainMenuSceneName = "StartMenu";
    public string gameSceneName = "Tutorial";

    public void ShowEndScreen()
    {
        Time.timeScale = 0f;
        endScreenPanel.SetActive(true);

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
        Time.timeScale = 1f;
        endScreenPanel.SetActive(false);
        ScoreManager.Instance?.ResetScore();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.SetActive(true);
            player.transform.position = new Vector3(9.4f, -3.54f, -44.8f);
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }

            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.ResetHealth();
            }
        }

        GameLoop gameLoop = FindObjectOfType<GameLoop>();
        if (gameLoop != null)
        {
            gameLoop.Reset();
        }

        SceneManager.LoadScene(gameSceneName);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f;
        endScreenPanel.SetActive(false);
        ScoreManager.Instance?.ResetScore();

        SceneManager.LoadScene(mainMenuSceneName);
    }
}