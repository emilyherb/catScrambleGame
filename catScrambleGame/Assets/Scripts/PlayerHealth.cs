using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Cat health initialized to: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Cat took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Cat died! Game Over!");

        // Show the End Screen
        GameOverManager gameOver = FindObjectOfType<GameOverManager>();
        if (gameOver != null)
        {
            gameOver.ShowEndScreen();
        }
        else
        {
            Debug.LogWarning("No GameOverManager found in scene!");
        }

        // Optionally deactivate the cat GameObject
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
public void ResetHealth()
{
    currentHealth = maxHealth;
    Debug.Log("Health reset to max: " + currentHealth);
}
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Powerup"))
    {
        RestoreHealth(1); // You could adjust this number if needed

        Destroy(other.gameObject); // Remove the powerup from the world
    }
}
public void RestoreHealth(int amount)
{
    currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    Debug.Log("Health restored by " + amount + ". Current health: " + currentHealth);
}


}
