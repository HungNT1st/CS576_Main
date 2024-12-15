using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUIManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider worldHealthSlider;
    public Slider playerHealthSlider;
    public TextMeshProUGUI worldHealthText;
    public TextMeshProUGUI playerHealthText;
    private float maxWorldHealth = 100f;
    private float maxPlayerHealth = 100f;

    void Start()
    {
        SetWorldHealth(0.5f * maxWorldHealth);
        SetPlayerHealth(maxPlayerHealth);
    }

    public void SetWorldHealth(float currentHealth)
    {
        if (worldHealthSlider == null || worldHealthText == null) return;

        float clampedHealth = Mathf.Clamp(currentHealth, 0f, maxWorldHealth);
        float normalizedValue = clampedHealth / maxWorldHealth;

        worldHealthSlider.value = normalizedValue;

        int percent = Mathf.RoundToInt(normalizedValue * 100f);
        worldHealthText.text = $"World Health: {percent}%";
    }

    public void SetPlayerHealth(float currentHealth)
    {
        if (playerHealthSlider == null || playerHealthText == null) return;

        float clampedHealth = Mathf.Clamp(currentHealth, 0f, maxPlayerHealth);
        float normalizedValue = clampedHealth / maxPlayerHealth;

        playerHealthSlider.value = normalizedValue;

        int percent = Mathf.RoundToInt(normalizedValue * 100f);
        playerHealthText.text = $"Player Health: {percent}%";
    }
}
