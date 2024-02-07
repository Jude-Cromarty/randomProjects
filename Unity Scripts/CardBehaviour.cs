using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{

  public enum Cards
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public Dictionary<Cards, string> cardObjectNames = new Dictionary<Cards, string>
    {
        { Cards.Two, "CardTwo" },
        { Cards.Three, "CardThree" },
        { Cards.Four, "CardFour" },
        { Cards.Five, "CardFive" },
        { Cards.Six, "CardSix" },
        { Cards.Seven, "CardSeven" },
        { Cards.Eight, "CardEight" },
        { Cards.Nine, "CardNine" },
        { Cards.Ten, "CardTen" },
        { Cards.Jack, "CardJack" },
        { Cards.Queen, "CardQueen" },
        { Cards.King, "CardKing" },
        { Cards.Ace, "CardAce" }
    };


   float Time_To_Destroy = 10f;
    void Start()
    {
        StartCoroutine(SelfDestruct());
        
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(Time_To_Destroy);
        Destroy(gameObject);
    }
    public Cards cardType; // Assign this in the Unity Editor

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            return;
        }
    }

    
        }
