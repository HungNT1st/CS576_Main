using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    public TextMeshProUGUI coinText;
    private int coins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinDisplay();
    }

    public void AddCoins(int amount)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAudioGroup("COIN COLLECT");
        }
        else {
        Debug.LogWarning("AudioManager.Instance is null when trying to play coin collect sound");
        }
        coins += amount;
        UpdateCoinDisplay();
    }

    public void RemoveCoins(int amount)
    {
        Debug.Log("Removed " + amount + " coins");
        coins -= amount;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = $"{coins}";
        }
    }

    public int GetCoins()
    {
        return coins;
    }
}
