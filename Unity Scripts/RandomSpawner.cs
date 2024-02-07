using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [System.Serializable]
    private class WeightedPrefab
    {
        public GameObject prefab;
        public float weight;
    }

    [SerializeField] private WeightedPrefab[] weightedPrefabs; // an array of prefabs to randomly choose from with their weights
    [SerializeField] private float spawnInterval = 2f; // the time interval between spawns
    [SerializeField] private GameObject spawnParent; // the parent transform to spawn the prefabs under
    private float timeSinceLastSpawn = 0f; // the time since the last spawn
    private float totalWeight = 0f; // the total weight of all the prefabs

    private Camera Cam;

    private void Start()
    {
         Cam = Camera.main;
        // Calculate the total weight of all the prefabs
        foreach (WeightedPrefab weightedPrefab in weightedPrefabs)
        {
            totalWeight += weightedPrefab.weight;
        }
        spawnParent = GameObject.FindWithTag("Player");
    }

    private void Update()
    {

Vector3 pos = Cam.WorldToViewportPoint(transform.position); // Set world space to camera bounds
pos.x = Mathf.Clamp01(pos.x);
pos.y = Mathf.Clamp01(pos.y); // Clamp between 0 - 1 for movement

var randomX = Random.Range(0f, 1f); // Get a random value between 0 and 1 for X
var randomY = Random.Range(0f, 1f); // Get a random value between 0 and 1 for Y

var position = Cam.ViewportToWorldPoint(new Vector3(randomX, randomY, Cam.nearClipPlane));

        timeSinceLastSpawn += Time.deltaTime; // increase the time since the last spawn

        // if the time since the last spawn is greater than the spawn interval
        if (timeSinceLastSpawn >= spawnInterval)
        {
            // Choose a random prefab from the array based on their weights
            float randomWeight = Random.Range(0f, totalWeight);
            WeightedPrefab chosenPrefab = null;
            foreach (WeightedPrefab weightedPrefab in weightedPrefabs)
            {
                randomWeight -= weightedPrefab.weight;
                if (randomWeight <= 0f)
                {
                    chosenPrefab = weightedPrefab;
                    break;
                }
            }

            // Instantiate the chosen prefab under the spawn parent at position (0, 0, 0)
            GameObject prefabToSpawn = chosenPrefab.prefab;
            Instantiate(prefabToSpawn, new Vector3(position.x, position.y, 0), Quaternion.identity);

            timeSinceLastSpawn = 0f; // reset the time since the last spawn
        }
    }
}
