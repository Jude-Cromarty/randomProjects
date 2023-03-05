using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourForPet : MonoBehaviour
{
    ///variables
    public int Hunger;
    public int Fun; 
    public int Energy;

    private int H, F, E;
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(Decreasing());  //STARTS COUNTDOWN
    }

    // Update is called once per frame
    void Update()
    {
        if(Hunger == 0)
        {
            Debug.Log("Hunger has reached " + Hunger);
        }
        if(Fun == 0)
        {
            Debug.Log("Fun has reached " + Fun); //SAYS WHEN COMPLETE
        }
        if(Energy == 0)
        {
            Debug.Log("Energy has reached " + Energy);
        }
    }

    IEnumerator Decreasing()
    {
        while(H < 1){Hunger -= 1; H++;} //CHECKS IF LESS THAN ONE THEN TAKES 1 IF SO
        while(F < 1){Fun -= 1; F++;}
        while(E < 1){Energy -= 1; E++;} 
        yield return new WaitForSeconds(1.0f); //PAUSES FOR 1 SECOND
        H = 0;//SETS VALUES BACK TO ZERO 
        F = 0;
        E = 0;
        StartCoroutine(Decreasing()); //STARTS LOOPS AGAIN
    }

    
}
