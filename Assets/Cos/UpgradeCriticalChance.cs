using UnityEngine;
using TMPro;

public class UpgradeCritChance : MonoBehaviour
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

        if (gm.SpendCoins(gm.critChancePrice))
        {
            gm.critChanceLevel++;
            gm.critChancePrice = Mathf.CeilToInt(gm.critChancePrice * 2.5f);

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
            "Increase Critical Chance +" +
            (gm.critChanceLevel * 0.5f).ToString("F1") + "%";

        priceText.text =
            "Price: " + gm.critChancePrice + " G";
    }
}