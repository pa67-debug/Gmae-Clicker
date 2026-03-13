using UnityEngine;
using TMPro;

public class UpgradeGoldMultiplier : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    public AudioSource audioSource;   // ตัวเล่นเสียง
    public AudioClip buySound;        // เสียงตอนซื้อ

    void Start()
    {
        UpdateUI();
    }

    public void Buy()
    {
        var gm = GameManager.Instance;

        if (gm.SpendCoins(gm.goldPrice))
        {
            gm.goldLevel++;
            gm.goldMultiplier++;
            gm.goldPrice *= 3;

            gm.SaveGameData();

            // 🔊 เล่นเสียงตอนซื้อ
            audioSource.PlayOneShot(buySound);

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        var gm = GameManager.Instance;

        descriptionText.text =
            "Increase Gold Gain x" + gm.goldMultiplier;

        priceText.text =
            "Price: " + gm.goldPrice + " G";
    }
}