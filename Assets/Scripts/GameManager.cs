using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : Singleton<GameManager>
{
    [Header("Post processing")]
    [SerializeField] PostProcessVolume volume;
    [SerializeField] Color goodColor;
    [SerializeField] Color badColor;

    [Header("Health")]
    public float maxWorldHealth = 100f;
    private float currentWorldHealth;

    public float maxPlayerHealth = 100f;
    private float currentPlayerHealth;

    void Start()
    {
        currentWorldHealth = 0.5f * maxWorldHealth;
        currentPlayerHealth = maxPlayerHealth;

        UpdateHealthUI();
        UpdateWorldPostProcessing();
    }

    public void DamageWorld(float amount)
    {
        currentWorldHealth = Mathf.Clamp(currentWorldHealth - amount, 0f, maxWorldHealth);
        UpdateHealthUI();
        UpdateWorldPostProcessing();

        if (currentWorldHealth <= 0)
        {
            Debug.Log("The world has died!");
            HUD.Instance.GameOver(false);
        }
    }

    public float GetCurrentWorldHealth() {
        return currentWorldHealth;
    }

    public void HealWorld(float amount)
    {
        currentWorldHealth = Mathf.Clamp(currentWorldHealth + amount, 0f, maxWorldHealth);
        UpdateHealthUI();
        UpdateWorldPostProcessing();
        if (currentWorldHealth >= maxWorldHealth) {
            HUD.Instance.GameOver(true);
        }
    }

    public void DamagePlayer(float amount)
    {
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth - amount, 0f, maxPlayerHealth);
        UpdateHealthUI();

        if (currentPlayerHealth <= 0)
        {
            Debug.Log("The player has died!");
            HUD.Instance.GameOver(false);
        }
    }

    public void HealPlayer(float amount)
    {
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth + amount, 0f, maxPlayerHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        HUD.Instance.SetPlayerHealth((float)(currentPlayerHealth/ maxPlayerHealth));
        HUD.Instance.SetEnvironmentHealth((float)(currentWorldHealth / maxWorldHealth));
    }
    private void UpdateWorldPostProcessing() {
        if (volume.profile.TryGetSettings<ColorGrading>(out var cg))
        {
            cg.colorFilter.overrideState = true;
            cg.colorFilter.value = Color.Lerp(badColor, goodColor, (float)(currentWorldHealth / maxWorldHealth));
        }
    }
}
