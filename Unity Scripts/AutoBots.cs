using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoBots : MonoBehaviour
{
    [System.Serializable]
    private class WeightedPrefab
    {
        public GameObject prefab;
    }
    private Slider selfHealthSlider;
    public static float IncreasePerSecond = 1;
    private float SP;
    private GameObject[] players;
    [SerializeField] private WeightedPrefab[] cardPrefabs = new WeightedPrefab[7]; // Set of card prefabs
    
    private float delayBetweenCardUsage; // Adjust the delay time as needed

    void Start()
    {
        SP = 100;
        players = GameObject.FindGameObjectsWithTag("Player");
GameObject self = gameObject;

GameObject selfHealthBar = self.transform.GetChild(0).gameObject;
selfHealthSlider = selfHealthBar.GetComponent<Slider>();

        StartCoroutine(UseCardsAutomatically());
    }


    void Update()
    {
        delayBetweenCardUsage = Random.Range(0,10);
        if(selfHealthSlider.value == 0){return;}

        if (SP < 200)
        {
            SP += IncreasePerSecond * Time.deltaTime;
           
        }
    }
    IEnumerator UseCardsAutomatically()
    {
        foreach (GameObject player in players)
        {
            DroppedObjectHandler handler = player.GetComponent<DroppedObjectHandler>();

            yield return new WaitForSeconds(delayBetweenCardUsage);
            ApplyRandomCardEffect(handler, player.name); // Pass player name to the function
        }
    }

    void ApplyRandomCardEffect(DroppedObjectHandler handler, string playerName)
    {
        if (cardPrefabs.Length > 0)
        {
            // Choose a random card prefab
            int randomIndex = Random.Range(0, cardPrefabs.Length);
            WeightedPrefab chosenCardPrefab = cardPrefabs[randomIndex];

            handler.HandleDroppedObject(chosenCardPrefab.prefab.name);
            SP -= chosenCardPrefab.prefab.GetComponent<DragMe>().CardCost;

            Debug.Log(gameObject.name.Replace("Card", "").Replace("(Clone)", "") + " Played " + chosenCardPrefab.prefab.name .Replace("Card", "").Replace("(Clone)", "") + " on " + playerName .Replace("Card", "").Replace("(Clone)", ""));
        }
    }
}
