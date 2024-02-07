using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    public GameObject prefab; // the prefab to instantiate
    private GameObject playerObj = null;
    
    private int allowedAmount = 1;
   List <GameObject> activeTurrets = new List <GameObject>();
void Start()
{
    
    if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");
}   
 public void SpawnPrefab()
    {
        if(activeTurrets.Count == allowedAmount)
        {
            Destroy(activeTurrets[0]);
            activeTurrets.RemoveAt(0);
        }
      
        Vector3 spawnPosition = new Vector3( playerObj.transform.position.x, playerObj.transform.position.y , 0);

        // instantiate the prefab at the player
        GameObject instantiatedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        activeTurrets.Add(instantiatedObject);
        
    }
     void Update() {
activeTurrets.RemoveAll(s => s == null);
    if (Input.GetButtonDown("Fire1"))
    {
        SpawnPrefab();
    }
}
}

