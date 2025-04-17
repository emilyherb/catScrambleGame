using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject endScreenPanel;
    public Text finalScoreText;
    public Text highScoreText;

    public GameObject startMenuCanvas;
    public GameObject mainPanel;
    public GameObject scoreCanvas;

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

    Time.timeScale = 1f;
    endScreenPanel.SetActive(false);
    scoreCanvas.SetActive(true);
    ScoreManager.Instance.ResetScore();

    GameObject player = GameObject.FindWithTag("Player");

    if (player != null)
    {
        // ✅ Step 1: Reactivate cat FIRST
        player.SetActive(true);
        Debug.Log("✅ Reactivated player");

        // ✅ Step 2: Then reset health
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

        // ✅ Step 3: Reset position & velocity
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
}




    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f;
        endScreenPanel.SetActive(false);
        startMenuCanvas.SetActive(true);
        mainPanel.SetActive(true);
        scoreCanvas.SetActive(false);
        ScoreManager.Instance.ResetScore();
    }
}
