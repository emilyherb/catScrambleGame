using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;

    public Image[] heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        currentHearts = maxHearts;
        UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        currentHearts -= amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        UpdateHearts();

        if (currentHearts <= 0)
        {
            GameOver();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < currentHearts ? fullHeart : emptyHeart;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}

