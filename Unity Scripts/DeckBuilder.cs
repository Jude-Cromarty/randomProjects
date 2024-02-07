using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public List<CardData> cardsOfDeck = new List<CardData>();
    }

    [System.Serializable]
    public class CardData
    {
        public string cardName; // Add other necessary information
    }

    public void SaveDeckToJson()
    {
        SaveData saveData = new SaveData();
        foreach (GameObject card in CardsInDeck)
        {
            // Populate SaveData with card information
            CardData cardData = new CardData();
            cardData.cardName = card.name; // Modify this based on your card data
            saveData.cardsOfDeck.Add(cardData);
        }

        string json = JsonUtility.ToJson(saveData);
        string path = Path.Combine(Application.persistentDataPath, "deckData.json");
        File.WriteAllText(path, json);
        Debug.Log("Deck Saved!");
    }

    public GameObject HighPriestess;
    public GameObject Emperor;
    public Text CardsSelected;
    private void Update()
    {
     
    }

    static List<GameObject> CardsInDeck = new List<GameObject>();
    public void cardSelected(string selectedCard)
    {
        switch (selectedCard)
        {
            case "HighPriestess":
                Debug.Log("High Priestess Added");
                CardsInDeck.Add(HighPriestess);
                break;
            case "Emperor":
                Debug.Log("Emperor Added");
                CardsInDeck.Add(Emperor); 
                break;
            default:
                break;
        }
    }



}
