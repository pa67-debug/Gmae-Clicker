using UnityEngine;
using TMPro;

public class UpgradeAttackSpeed : MonoBehaviour
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

        if (gm.SpendCoins(gm.speedPrice))
        {
            gm.speedLevel++;
            gm.speedPrice *= 2;

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
            "Increase Attack Speed +" + gm.speedLevel;

        priceText.text =
            "Price: " + gm.speedPrice + " G";
    }
}