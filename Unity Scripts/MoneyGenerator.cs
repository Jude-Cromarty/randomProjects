using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour
{
    public double moneyGeneratedPerSecond; // the amount of money generated per second

    private MoneyManager moneyManager; // reference to the money manager

    void Start()
    {
        // find the money manager and call the MoneyGeneratorAdded method
        moneyManager = FindObjectOfType<MoneyManager>();
        moneyManager.AddGenerator(this);
    }

    public double GetMoneyGeneratedPerSecond()
    {
        // return the amount of money generated per second
        return moneyGeneratedPerSecond;
    }
}
