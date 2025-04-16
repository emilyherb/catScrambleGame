using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthSlider;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth not assigned to HealthBarUI!");
            return;
        }

        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider not assigned to HealthBarUI!");
            return;
        }
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        healthSlider.value = playerHealth.GetCurrentHealth();
    }

    void Update()
    {
        if (playerHealth != null)
        {
            healthSlider.value = playerHealth.GetCurrentHealth();
        }
    }
}