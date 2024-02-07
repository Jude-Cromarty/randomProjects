using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct MoneyData
{
    public GameObject moneyObject;
    public int nonPremiumAmount;
    public int premiumAmount;
}

public class CurrencyController : MonoBehaviour
{
    public int nonPremiumCurrencyAmount = 0;
    public int premiumCurrencyAmount = 0;

    public Text nonPremiumText;
    public Text premiumText;
    public Text nonPremiumPanelText;
    public Text premiumPanelText;
    public GameObject placedObjects;
    public float moneyGenerationTime = 5f; // time in seconds until money is generated
    public MoneyData[] moneyData; // array of money generation data

    private float timer = 0f;

    void Start()
    {
        UpdateCurrencyText();
    }

    void Update()
    {
        // update timer
        timer += Time.deltaTime;

        // check if it's time to generate money
        if (timer >= moneyGenerationTime)
        {
            GenerateMoney();
            timer = 0f;
        }
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

    public bool CanAfford(int cost)
    {
        return nonPremiumCurrencyAmount >= cost;
    }

    public bool CanAffordPremium(int cost)
    {
        return premiumCurrencyAmount >= cost;
    }

    void GenerateMoney()
{
   
    // loop through all active children of PlacedObjects and generate money based on MoneyData array
    for (int i = 0; i < placedObjects.transform.childCount; i++)
    {
        GameObject child = placedObjects.transform.GetChild(i).gameObject;
        // Only process active children
        if (child.activeSelf)
        {
            for (int j = 0; j < moneyData.Length; j++)
            {
                if (moneyData[j].moneyObject == child)
                {
                    if (child.CompareTag("Premium"))
                    {
                        premiumCurrencyAmount += moneyData[j].premiumAmount;
                    }
                    else
                    {
                        nonPremiumCurrencyAmount += moneyData[j].nonPremiumAmount;
                    }
                }
            }
        }
    }

    UpdateCurrencyText();
}
}

