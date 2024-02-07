using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    
    int maxCards = 6;
    public bool Flipped;
    public static List<GameObject> currentCards = new List<GameObject>();

    public void Flip()
    {
        if(!Flipped||currentCards.Count < maxCards){
            Flipped = true;
    //get cards in container
    GameObject originalGameObject = GameObject.Find("CardContainer");
    for (int j = 0; j < originalGameObject.transform.childCount; j++)
    {
        GameObject child = originalGameObject.transform.GetChild(j).gameObject;
        currentCards.Add(child.gameObject);
    }
    //flip cards
    for (int i = 0; i < currentCards.Count; i++) 
    {
            currentCards[i].transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180);
    }     
        }
    else if(Flipped = true || currentCards.Count >= maxCards)
    {
        currentCards.Clear();
        Flipped = false;
            //get cards in container
    GameObject originalGameObject = GameObject.Find("CardContainer");
    for (int j = 0; j < originalGameObject.transform.childCount; j++)
    {
        GameObject child = originalGameObject.transform.GetChild(j).gameObject;
        currentCards.Add(child.gameObject);
    }
    //flip cards
    for (int i = 0; i < currentCards.Count; i++) 
    {
        Flipped = true;
            currentCards[i].transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 360);
            Flipped = false;
    }     
    }
    }

}
