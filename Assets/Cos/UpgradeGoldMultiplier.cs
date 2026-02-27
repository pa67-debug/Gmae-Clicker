using UnityEngine;
using TMPro;

public class UpgradeGoldMultiplier : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    private int level = 0;
    private int price = 3000;

    void Start()
    {
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if (GameManager.Instance.SpendCoins(price))
        {
            level++;

            GameManager.Instance.goldMultiplier += 1;

            price = Mathf.CeilToInt(price * 3f);

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
            "Increase Gold Gain\nCurrent: x" + GameManager.Instance.goldMultiplier;

        priceText.text = "Price: " + price + " G";
    }
}