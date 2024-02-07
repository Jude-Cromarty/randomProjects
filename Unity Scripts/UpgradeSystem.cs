using UnityEngine;
using System.Collections.Generic;

public class UpgradeSystem : MonoBehaviour
{
    // Singleton instance
    public static UpgradeSystem Instance;

    [Header("Upgrade Settings")]
    public int clickDamageUpgradeCost = 10;
    public float clickDamageMultiplier = 1.2f;
    public int passiveIncomeUpgradeCost = 20;
    public float passiveIncomeMultiplier = 1.5f;

    // Dictionary to store the upgrade data
    private Dictionary<string, UpgradeData> upgradeDictionary = new Dictionary<string, UpgradeData>();

    private void Awake()
    {
        // Ensure there is only one instance of this script
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to not be destroyed on scene load
        DontDestroyOnLoad(gameObject);

        // Initialize the upgrade dictionary with upgrade data
        InitializeUpgradeDictionary();
    }

    private void InitializeUpgradeDictionary()
    {
        // Add click damage upgrade data to the dictionary
        UpgradeData clickDamageUpgrade = new UpgradeData("Click Damage", clickDamageUpgradeCost, clickDamageMultiplier);
        upgradeDictionary.Add("click_damage", clickDamageUpgrade);

        // Add passive income upgrade data to the dictionary
        UpgradeData passiveIncomeUpgrade = new UpgradeData("Passive Income", passiveIncomeUpgradeCost, passiveIncomeMultiplier);
        upgradeDictionary.Add("passive_income", passiveIncomeUpgrade);
    }

    public void PurchaseUpgrade(string upgradeKey)
    {
        if (GameManager.Instance.playerData.Coins >= upgradeDictionary[upgradeKey].cost)
        {
            // Deduct the cost of the upgrade from the player's coins
            GameManager.Instance.playerData.Coins -= upgradeDictionary[upgradeKey].cost;

            // Apply the upgrade to the player's data
            if (upgradeKey == "click_damage")
            {
                GameManager.Instance.playerData.ClickDamage = Mathf.RoundToInt(GameManager.Instance.playerData.ClickDamage * upgradeDictionary[upgradeKey].multiplier);
            }
            else if (upgradeKey == "passive_income")
            {
                GameManager.Instance.playerData.PassiveIncome = Mathf.RoundToInt(GameManager.Instance.playerData.PassiveIncome * upgradeDictionary[upgradeKey].multiplier);
            }

            // Increase the cost of the upgrade for the next purchase
            upgradeDictionary[upgradeKey].cost = Mathf.RoundToInt(upgradeDictionary[upgradeKey].cost * upgradeDictionary[upgradeKey].multiplier);

            // Save the player data to file
            SaveSystem.SaveData(GameManager.Instance.playerData);

            // Update the UI with the new player data
            UIManager.Instance.UpdatePlayerData();
        }
    }

    public UpgradeData GetUpgradeData(string upgradeKey)
    {
        return upgradeDictionary[upgradeKey];
    }
}

public class UpgradeData
{
    public string name;
    public int cost;
    public float multiplier;

    public UpgradeData(string upgradeName, int upgradeCost, float upgradeMultiplier)
    {
        name = upgradeName;
        cost = upgradeCost;
        multiplier = upgradeMultiplier;
    }
}
