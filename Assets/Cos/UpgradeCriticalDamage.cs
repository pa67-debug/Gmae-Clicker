using UnityEngine;
using TMPro;

public class UpgradeCritDamage : MonoBehaviour
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

        if (gm.SpendCoins(gm.critDamagePrice))
        {
            gm.critDamageLevel++;
            gm.critDamagePrice =
                Mathf.CeilToInt(gm.critDamagePrice * 2.5f);

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
            "Increase Critical Damage x" +
            (2f + gm.critDamageLevel);

        priceText.text =
            "Price: " + gm.critDamagePrice + " G";
    }
}