using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListManager : MonoBehaviour
{
    public GameObject deckListPrefab, deckListLayout;
    static List<GameObject> deckList = new List<GameObject>();

public void newDeck()
{
    Instantiate(deckListPrefab, transform.position, Quaternion.identity, deckListLayout.transform);
}

}
