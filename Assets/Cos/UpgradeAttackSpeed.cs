using UnityEngine;
using TMPro;

public class UpgradeAttackSpeed : MonoBehaviour
{
    public PlayerAttack player;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    private int level = 0;
    private int price = 120;

    private float reduceAmount = 0.5f; // ลดเวลา 0.5 วิ ต่อครั้ง

    void Start()
    {
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if (GameManager.Instance.SpendCoins(price))
        {
            level++;

            player.ReduceAutoAttackTime(reduceAmount);

            price *= 2;

            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    void UpdateUI()
    {
        descriptionText.text = "Increase Attack Speed +" + level;
        priceText.text = "Price: " + price + " G";
    }
}