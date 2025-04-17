using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private float scoreTimer = 0f;

    void Update()
    {
        if (Time.timeScale > 0f) // Only while game is playing
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 1f)
            {
                ScoreManager.Instance.AddPoints(1); // Add 1 point per second
                scoreTimer = 0f;
            }
        }
    }
}

