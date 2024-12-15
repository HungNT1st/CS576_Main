using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HealthUIManager healthUIManager;

    public float maxWorldHealth = 100f;
    private float currentWorldHealth;

    public float maxPlayerHealth = 100f;
    private float currentPlayerHealth;

    void Start()
    {
        currentWorldHealth = 0.5f * maxWorldHealth;
        currentPlayerHealth = maxPlayerHealth;

        UpdateHealthUI();
    }

    public void DamageWorld(float amount)
    {
        currentWorldHealth = Mathf.Clamp(currentWorldHealth - amount, 0f, maxWorldHealth);
        UpdateHealthUI();

        if (currentWorldHealth <= 0)
        {
            Debug.Log("The world has died!");
            Time.timeScale = 0f;
        }
    }

    public void HealWorld(float amount)
    {
        currentWorldHealth = Mathf.Clamp(currentWorldHealth + amount, 0f, maxWorldHealth);
        UpdateHealthUI();
    }

    public void DamagePlayer(float amount)
    {
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth - amount, 0f, maxPlayerHealth);
        UpdateHealthUI();

        if (currentPlayerHealth <= 0)
        {
            Debug.Log("The player has died!");
            Time.timeScale = 0f;
        }
    }

    public void HealPlayer(float amount)
    {
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth + amount, 0f, maxPlayerHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthUIManager != null)
        {
            healthUIManager.SetWorldHealth(currentWorldHealth);
            healthUIManager.SetPlayerHealth(currentPlayerHealth);
        }
        else
        {
            Debug.LogWarning("No HealthUIManager assigned to GameManager!");
        }
    }
}