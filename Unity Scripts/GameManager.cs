using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int level;
    public static int money;
    public static int multiplier;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        money = 0;
        multiplier = 1;
    }
}
