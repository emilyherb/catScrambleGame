using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private float scoreTimer = 0f;
    private bool isGameActive = true;

    void Update()
    {
        if (isGameActive && Time.timeScale > 0f)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 1f)
            {
                ScoreManager.Instance.AddPoints(1);
                scoreTimer = 0f;
            }
        }
    }

    public void StopScoring()
    {
        isGameActive = false;
    }

    public void Reset()
    {
        isGameActive = true;
        scoreTimer = 0f;
    }
}
