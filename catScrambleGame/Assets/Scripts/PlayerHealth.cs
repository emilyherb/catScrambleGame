using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip powerupSound;
    public AudioClip damageSound;

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

    if (audioSource != null && deathSound != null)
    {
        audioSource.PlayOneShot(deathSound);
    }
    else
    {
        Debug.LogWarning("Missing audio source or death sound!");
    }

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
        if (audioSource != null && powerupSound != null)
    {
        audioSource.PlayOneShot(powerupSound);
    }
    else
    {
        Debug.LogWarning("Missing audio source or powerup sound!");
    }
    }
    if (other.CompareTag("Obstacle"))
    {
        if (audioSource != null && damageSound != null)
    {
        audioSource.PlayOneShot(damageSound);
    }
    else
    {
        Debug.LogWarning("Missing audio source or damage sound!");
    }
    }

}
public void RestoreHealth(int amount)
{
    currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    Debug.Log("Health restored by " + amount + ". Current health: " + currentHealth);
}


}
