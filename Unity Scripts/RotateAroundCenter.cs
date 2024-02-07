using UnityEngine;

public class RotateAroundCenter : MonoBehaviour
{
    private GameObject player;
    public float rotationSpeed = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Alice");
    }
    private void Update()
    {

        // Rotate the object around the center point
        transform.RotateAround(player.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

