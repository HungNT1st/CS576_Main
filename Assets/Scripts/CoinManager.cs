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
        AudioManager.Instance.PlayAudioGroup("COIN COLLECT");
        coins += amount;
        UpdateCoinDisplay();
    }

    public void RemoveCoins(int amount)
    {
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
