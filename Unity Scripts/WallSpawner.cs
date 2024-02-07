using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstaclePrefab
{
    public GameObject prefab;
    public int maxCount;
}

public class ObstacleSpawner : MonoBehaviour
{
    public List<ObstaclePrefab> obstaclePrefabs; // List of custom obstacle prefabs with their maximum counts.
    public float spawnDistance = 10f; // Distance ahead of the camera to spawn new obstacles.
    public float minGap = 2f; // Minimum gap between obstacles.
    public float maxGap = 5f; // Maximum gap between obstacles.

    private List<GameObject> spawnedObstacles = new List<GameObject>();

    private void Update()
    {
        // Get the camera's position in the world.
        Vector3 cameraPosition = Camera.main.transform.position;

        // Check if the spawner is below the camera.
        if (transform.position.y < cameraPosition.y)
        {
            // Move the spawner below the camera by spawnDistance.
            transform.position = new Vector3(cameraPosition.x, cameraPosition.y - spawnDistance, cameraPosition.z);

            // Spawn obstacles.
            SpawnObstacles();
        }

        // Check if any spawned obstacle is above the camera and not in view to delete it.
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = spawnedObstacles[i];
            if (obstacle.transform.position.y > cameraPosition.y && !IsInView(obstacle.GetComponent<Renderer>()))
            {
                Destroy(obstacle);
                spawnedObstacles.RemoveAt(i);
            }
        }
    }

    private void SpawnObstacles()
    {
        // Determine the X positions for the left and right obstacles.
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float leftSpawnX = Random.Range(-halfWidth, -halfWidth / 2f);
        float rightSpawnX = Random.Range(halfWidth / 2f, halfWidth);

        // Calculate random gaps for each obstacle pair.
        float gapLeft = Random.Range(minGap, maxGap);
        float gapRight = Random.Range(minGap, maxGap);

        // Spawn the left obstacle if there's no overlapping obstacle with the same tag.
        Vector3 leftSpawnPosition = new Vector3(leftSpawnX, transform.position.y + gapLeft, 0f);
        if (!IsOverlappingWithTag(leftSpawnPosition, "Obstacle"))
        {
            GameObject leftObstaclePrefab = GetRandomObstaclePrefab();
            GameObject leftObstacle = Instantiate(leftObstaclePrefab, leftSpawnPosition, Quaternion.identity);
            spawnedObstacles.Add(leftObstacle);
        }

        // Spawn the right obstacle if there's no overlapping obstacle with the same tag.
        Vector3 rightSpawnPosition = new Vector3(rightSpawnX, transform.position.y + gapRight, 0f);
        if (!IsOverlappingWithTag(rightSpawnPosition, "Obstacle"))
        {
            GameObject rightObstaclePrefab = GetRandomObstaclePrefab();
            GameObject rightObstacle = Instantiate(rightObstaclePrefab, rightSpawnPosition, Quaternion.identity);
            spawnedObstacles.Add(rightObstacle);
        }
    }

    private bool IsInView(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    private bool IsOverlappingWithTag(Vector3 position, string tag)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    private GameObject GetRandomObstaclePrefab()
    {
        List<ObstaclePrefab> availablePrefabs = new List<ObstaclePrefab>();
        foreach (var obstaclePrefab in obstaclePrefabs)
        {
            if (spawnedObstacles.FindAll(o => o.CompareTag(obstaclePrefab.prefab.tag)).Count < obstaclePrefab.maxCount)
            {
                availablePrefabs.Add(obstaclePrefab);
            }
        }

        if (availablePrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, availablePrefabs.Count);
            return availablePrefabs[randomIndex].prefab;
        }

        return null;
    }
}
