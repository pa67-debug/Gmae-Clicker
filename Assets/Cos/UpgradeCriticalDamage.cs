using UnityEngine;
using TMPro;

public class UpgradeCriticalDamage : MonoBehaviour
{
    public PlayerAttack player;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    private int level = 0;
    private int price = 1000;

    void Start()
    {
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if (GameManager.Instance.SpendCoins(price))
        {
            level++;

            // เพิ่มแรงคริ +1 เท่าทุกครั้ง
            player.critMultiplier += 1f;

            // ราคา x2.5 และปัดขึ้น
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
            "Increase Critical Damage\nCurrent: x" + player.critMultiplier;

        priceText.text = "Price: " + price + " G";
    }
}