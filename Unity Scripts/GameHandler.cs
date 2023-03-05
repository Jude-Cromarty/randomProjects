using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public Text Money;

    public void Increment()
    {
    GameManager.money += GameManager.multiplier;

    }
    public void Buy(int num)
    {
        if(num == 1)
        {

        }
        if(num == 2)
        {

        }
        if(num == 3)
        {

        }
    }

    void Update()
    {
        Money.text = "Money: " + GameManager.money;
    }

}
