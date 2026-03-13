using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Currency")]
    public int coins;
    public int goldMultiplier = 1;
    public TextMeshProUGUI coinText;

    [Header("Upgrade Data")]
    public int weaponLevel;
    public int weaponPrice = 100;

    public int speedLevel;
    public int speedPrice = 120;

    public int critChanceLevel;
    public int critChancePrice = 300;

    public int critDamageLevel;
    public int critDamagePrice = 1000;

    public int goldLevel;
    public int goldPrice = 3000;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadGameData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        coinText = GameObject.FindWithTag("CoinText")?.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = coins + " G";
    }

    public void AddCoins(int amount)
    {
        coins += amount * goldMultiplier;
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

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("GoldMultiplier", goldMultiplier);

        PlayerPrefs.SetInt("WeaponLevel", weaponLevel);
        PlayerPrefs.SetInt("WeaponPrice", weaponPrice);

        PlayerPrefs.SetInt("SpeedLevel", speedLevel);
        PlayerPrefs.SetInt("SpeedPrice", speedPrice);

        PlayerPrefs.SetInt("CritChanceLevel", critChanceLevel);
        PlayerPrefs.SetInt("CritChancePrice", critChancePrice);

        PlayerPrefs.SetInt("CritDamageLevel", critDamageLevel);
        PlayerPrefs.SetInt("CritDamagePrice", critDamagePrice);

        PlayerPrefs.SetInt("GoldLevel", goldLevel);
        PlayerPrefs.SetInt("GoldPrice", goldPrice);

        PlayerPrefs.Save();
    }

    void LoadGameData()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        goldMultiplier = PlayerPrefs.GetInt("GoldMultiplier", 1);

        weaponLevel = PlayerPrefs.GetInt("WeaponLevel", 0);
        weaponPrice = PlayerPrefs.GetInt("WeaponPrice", 100);

        speedLevel = PlayerPrefs.GetInt("SpeedLevel", 0);
        speedPrice = PlayerPrefs.GetInt("SpeedPrice", 120);

        critChanceLevel = PlayerPrefs.GetInt("CritChanceLevel", 0);
        critChancePrice = PlayerPrefs.GetInt("CritChancePrice", 300);

        critDamageLevel = PlayerPrefs.GetInt("CritDamageLevel", 0);
        critDamagePrice = PlayerPrefs.GetInt("CritDamagePrice", 1000);

        goldLevel = PlayerPrefs.GetInt("GoldLevel", 0);
        goldPrice = PlayerPrefs.GetInt("GoldPrice", 3000);
    }
}