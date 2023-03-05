using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour
{
    public int nonPremiumCurrencyAmount = 0;
    public int premiumCurrencyAmount = 0;

    public Text nonPremiumText;
    public Text premiumText;
    public Text nonPremiumPanelText;
    public Text premiumPanelText;

    void Start()
    {
        UpdateCurrencyText();
    }

    void UpdateCurrencyText()
    {
        nonPremiumText.text = nonPremiumCurrencyAmount.ToString();
        nonPremiumPanelText.text = nonPremiumCurrencyAmount.ToString();

        premiumText.text = premiumCurrencyAmount.ToString();
        premiumPanelText.text = premiumCurrencyAmount.ToString();
    }

    public void SpendNonPremiumCurrency(int amount)
    {
        nonPremiumCurrencyAmount -= amount;
        UpdateCurrencyText();
    }

    public void SpendPremiumCurrency(int amount)
    {
        premiumCurrencyAmount -= amount;
        UpdateCurrencyText();
    }
}
