using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    int cha;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("up"))
        {
        cha = Random.Range(1, 6);
        string[] currentPets = {"Rock", "Fox", "Cat", "Dog", "Dragon", "Hamster"};
        Debug.Log(currentPets[cha]);
        }
    }
}
