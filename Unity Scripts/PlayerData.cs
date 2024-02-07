using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Currency")]
    [SerializeField] private int coins;
    [SerializeField] private int gems;

    [Header("Level")]
    [SerializeField] private int level;
    [SerializeField] private int experience;

    [Header("Upgrades")]
    [SerializeField] private int clickDamage;
    [SerializeField] private int passiveIncome;

    // Properties to access the private variables
    public int Coins { get { return coins; } set { coins = value; } }
    public int Gems { get { return gems; } set { gems = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int Experience { get { return experience; } set { experience = value; } }
    public int ClickDamage { get { return clickDamage; } set { clickDamage = value; } }
    public int PassiveIncome { get { return passiveIncome; } set { passiveIncome = value; } }

    // Resets the player data to its default values
    public void ResetData()
    {
        coins = 0;
        gems = 0;
        level = 1;
        experience = 0;
        clickDamage = 1;
        passiveIncome = 0;
    }
}
