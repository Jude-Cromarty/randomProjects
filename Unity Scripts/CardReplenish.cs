using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardReplenish : MonoBehaviour
{
    [System.Serializable]
    private class WeightedPrefab
    {
        public GameObject prefab;
    }
    
    private GameObject container;
    static int maxCards = 5;
     [SerializeField] private WeightedPrefab[] weightedPrefabs  = new WeightedPrefab[11]; //deck size of 7 //deck size of 7

    // Make currentCards static
    static List<GameObject> currentCards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        container = GameObject.Find("CardContainer");
        GetCurrent();

        // Ensure there are always maxCards in the scene
        InstantiateMissingCards();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrent();

        if (currentCards.Count < maxCards)
        {
            // Calculate how many cards are missing
            int cardsToInstantiate = maxCards - currentCards.Count;

            // Instantiate the missing cards
            for (int i = 0; i < cardsToInstantiate; i++)
            {
                // Choose a random prefab
                int randomIndex = Random.Range(0, weightedPrefabs.Length);
                WeightedPrefab chosenPrefab = weightedPrefabs[randomIndex];

                if (chosenPrefab != null)
                {
                    // Instantiate the chosen prefab
                    GameObject prefabToSpawn = chosenPrefab.prefab;
                    Instantiate(prefabToSpawn, new Vector3(0, 0, 0), Quaternion.identity, container.transform);
                }
            }
        }
    }

    void GetCurrent()
    {
        currentCards.Clear();

        for (int j = 0; j < container.transform.childCount; j++)
        {
            GameObject child = container.transform.GetChild(j).gameObject;
            currentCards.Add(child);
        }
    }

    void InstantiateMissingCards()
    {
        // Calculate how many cards are missing
        int cardsToInstantiate = maxCards - currentCards.Count;

        // Instantiate the missing cards
        for (int i = 0; i < cardsToInstantiate; i++)
        {
            // Choose a random prefab
            int randomIndex = Random.Range(0, weightedPrefabs.Length);
            WeightedPrefab chosenPrefab = weightedPrefabs[randomIndex];

            if (chosenPrefab != null)
            {
                // Instantiate the chosen prefab
                GameObject prefabToSpawn = chosenPrefab.prefab;
                Instantiate(prefabToSpawn, new Vector3(0, 0, 0), Quaternion.identity, container.transform);
            }
        }
    }


  /*    void LoadSavedCards()
    {
        string path = Path.Combine(Application.persistentDataPath, "deckData.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DeckBuilder.SaveData saveData = JsonUtility.FromJson<DeckBuilder.SaveData>(json);

            foreach (DeckBuilder.CardData cardData in saveData.cardsOfDeck)
            {
                // Instantiate cards based on cardData information
                for (int i = 0; i < weightedPrefabs.Length; i++)
                {
                    if (weightedPrefabs[i].cardName == cardData.cardName)
                    {
                        Instantiate(weightedPrefabs[i].prefab, new Vector3(0, 0, 0), Quaternion.identity, container.transform);
                        break;
                    }
                }
            }
        }
    }*/

}
