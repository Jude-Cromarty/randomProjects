using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class InfiniteLevelGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs (collectibles, failures, and generic obstacles).
    public Transform player; // Reference to the player's transform.
    public float spawnDistance = 10f; // Distance ahead of the player to spawn new segments.
    public float destroyDistance = 10f; // Distance behind the player to destroy segments.
    public int maxStoredObjects = 5; // Maximum number of last objects' positions to store.
    public float minGap = 1.5f; // Minimum gap between obstacles.
    public float maxGap = 3.5f; // Maximum gap between obstacles.

    private List<float> lastSpawnPositions = new List<float>(); // List to store the last few spawn positions.

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the list with the player's starting position.
        lastSpawnPositions.Add(player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved beyond the spawn distance.
        if (player.position.y < lastSpawnPositions[0] - spawnDistance) // Negate spawnDistance.
        {
            SpawnNextSegments();
        }

        // Check if the player has moved beyond the destroy distance.
      // Check if the player has moved beyond the destroy distance.
if (player.position.y < lastSpawnPositions[lastSpawnPositions.Count - 1] + destroyDistance)
{
    Debug.Log("Attempting");
    StartCoroutine(DestroySegments());
}

    }

    private void SpawnNextSegments()
{
    // Randomly select an obstacle prefab from the array.
    GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

    // Determine the X positions for the left and right obstacles.
    float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    float leftSpawnX = Random.Range(-halfWidth, -halfWidth / 2f);
    float rightSpawnX = Random.Range(halfWidth / 2f, halfWidth);

    // Calculate a random gap between obstacles.
    float gap = Random.Range(minGap, maxGap);

    // Calculate the Y position of the new obstacle based on the last stored position and the gap.
    float newSpawnY = lastSpawnPositions[0] - spawnDistance - gap; // Negate spawnDistance.

    // Spawn the left obstacle.
    Vector3 leftSpawnPosition = new Vector3(leftSpawnX, newSpawnY, 0f);
    Instantiate(obstaclePrefab, leftSpawnPosition, Quaternion.identity);

    // Spawn the right obstacle.
    Vector3 rightSpawnPosition = new Vector3(rightSpawnX, newSpawnY, 0f);
    GameObject rightObstacle = Instantiate(obstaclePrefab, rightSpawnPosition, Quaternion.identity);

    // Mirror the right obstacle by flipping its x-scale.
    Vector3 rightScale = rightObstacle.transform.localScale;
    rightScale.x = -rightScale.x;
    rightObstacle.transform.localScale = rightScale;

    // Add the new obstacle position to the list.
    lastSpawnPositions.Insert(0, newSpawnY);

    // If the list exceeds the maximum size, remove the last item.
    if (lastSpawnPositions.Count > maxStoredObjects)
    {
        lastSpawnPositions.RemoveAt(lastSpawnPositions.Count - 1);
    }
}


    private IEnumerator DestroySegments()
    {
        // Find all obstacle segments and destroy them if they are behind the player.
        GameObject[] obstacleSegments = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacleSegment in obstacleSegments)
        {
            yield return new WaitForSeconds(1f);
                
                Destroy(obstacleSegment);

                Debug.Log("Destroyed");
                 
            
        }
        
    }
}
