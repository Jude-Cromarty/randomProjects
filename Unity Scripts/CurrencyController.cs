using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour
{
    public int nonPremiumBalance = 100;
    public int premiumBalance = 0;

    public Text nonPremiumText;
    public Text premiumText;

    void Start()
    {
    UpdateCurrencyText();
    }

    public void SpendCurrency(bool isPremium, int amount)
    {
        if (isPremium)
        {
            if (premiumBalance >= amount)
            {
                premiumBalance -= amount;
            }
            else
            {
                Debug.Log("Not enough premium currency!");
                return;
            }
        }
        else
        {
            if (nonPremiumBalance >= amount)
            {
                nonPremiumBalance -= amount;
            }
            else
            {
                Debug.Log("Not enough non-premium currency!");
                return;
            }
        }

        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        nonPremiumText.text = "Non-Premium: " + nonPremiumBalance;
        premiumText.text = "Premium: " + premiumBalance;
    }
}
