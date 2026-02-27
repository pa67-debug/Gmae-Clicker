using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Currency")]
    public int coins = 0;
    public int goldMultiplier = 1;
    public TextMeshProUGUI coinText;

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
            return;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    // =====================
    // 💰 เงิน
    // =====================

    public void AddCoins(int amount)
    {
        int finalAmount = amount * goldMultiplier;
        coins += finalAmount;
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = coins + " G";
    }

    // =====================
    // 💾 SAVE / LOAD
    // =====================

    public void SaveGame(PlayerAttack player)
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("GoldMultiplier", goldMultiplier);

        PlayerPrefs.SetInt("Damage", player.damage);
        PlayerPrefs.SetFloat("CritChance", player.critChance);
        PlayerPrefs.SetFloat("CritMultiplier", player.critMultiplier);
        PlayerPrefs.SetFloat("AutoAttackInterval", player.autoAttackInterval);

        PlayerPrefs.Save();
    }

    public void LoadGame(PlayerAttack player)
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        goldMultiplier = PlayerPrefs.GetInt("GoldMultiplier", 1);

        player.damage = PlayerPrefs.GetInt("Damage", 1);
        player.critChance = PlayerPrefs.GetFloat("CritChance", 0f);
        player.critMultiplier = PlayerPrefs.GetFloat("CritMultiplier", 2f);
        player.autoAttackInterval = PlayerPrefs.GetFloat("AutoAttackInterval", 60f);

        UpdateUI();
    }
}