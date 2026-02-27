using UnityEngine;
using TMPro;

public class UpgradeWeapon : MonoBehaviour
{
    public PlayerAttack player;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    private int level = 0;
    private int price = 100;

    void Start()
    {
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if (GameManager.Instance.SpendCoins(price))
        {
            level++;
            player.damage += 1;

            price = Mathf.RoundToInt(price * 1.5f);

            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    void UpdateUI()
    {
        descriptionText.text = "Upgrade Weapon +" + (level + 1) + " Damage";
        priceText.text = "Price: " + price + " G";
    }
}