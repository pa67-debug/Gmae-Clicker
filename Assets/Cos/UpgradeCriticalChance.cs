using UnityEngine;
using TMPro;

public class UpgradeCriticalChance : MonoBehaviour
{
    public PlayerAttack player;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    private int level = 0;
    private int price = 300;

    private float increaseAmount = 0.5f; // เพิ่มคริ 0.5%

    void Start()
    {
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if (GameManager.Instance.SpendCoins(price))
        {
            level++;

            player.critChance += increaseAmount;

            // คูณราคา x2.5 และปัดขึ้น
            price = Mathf.CeilToInt(price * 2.5f);

            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    void UpdateUI()
    {
        descriptionText.text =
            "Increase Critical Chance +" + (level * increaseAmount).ToString("F1") + "%";

        priceText.text = "Price: " + price + " G";
    }
}