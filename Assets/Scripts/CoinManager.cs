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
        coins += amount;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {coins}";
        }
    }

    public int GetCoins()
    {
        return coins;
    }
}
