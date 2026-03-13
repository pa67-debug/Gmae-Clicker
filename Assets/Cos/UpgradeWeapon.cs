using UnityEngine;
using TMPro;

public class UpgradeWeapon : MonoBehaviour
{
    public PlayerAttack player;
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

        if (gm.SpendCoins(gm.weaponPrice))
        {
            gm.weaponLevel++;
            gm.weaponPrice = Mathf.CeilToInt(gm.weaponPrice * 1.5f);

            gm.SaveGameData();

            player.damage = 1 + gm.weaponLevel;

            // 🔊 เล่นเสียงตอนซื้อ
            audioSource.PlayOneShot(buySound);

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        var gm = GameManager.Instance;

        descriptionText.text =
            "Upgrade Weapon +" + (gm.weaponLevel + 1) + " Damage";

        priceText.text =
            "Price: " + gm.weaponPrice + " G";
    }
}