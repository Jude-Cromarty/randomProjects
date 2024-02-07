using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

[SerializeField] private Text coinsText;
[SerializeField] private Text clickDamageText;
[SerializeField] private Text passiveIncomeText;
[SerializeField] private Text levelText;
[SerializeField] private Text gemsText;
[SerializeField] private Text experienceText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

public void UpdatePlayerData()
{
    coinsText.text = "Coins: " + GameManager.Instance.playerData.Coins.ToString();
    clickDamageText.text = "Click Damage: " + GameManager.Instance.playerData.ClickDamage.ToString();
    passiveIncomeText.text = "Passive Income: " + GameManager.Instance.playerData.PassiveIncome.ToString();
    levelText.text = "Level: " + GameManager.Instance.playerData.Level.ToString();
    gemsText.text = "Gems: " + GameManager.Instance.playerData.Gems.ToString();
    experienceText.text = "Experience: " + GameManager.Instance.playerData.Experience.ToString();
}

}
