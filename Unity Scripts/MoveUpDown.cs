using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float amplitude = 1.0f; // The amplitude of the up and down movement
    public float frequency = 1.0f; // The frequency of the up and down movement

    private Vector3 initialPosition;

    private void Start()
    {
        // Store the initial position of the objects
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the vertical movement based on sine function
        float verticalMovement = amplitude * Mathf.Sin(Time.time * frequency);

        // Update the position of the objects with the vertical movement
        Vector3 newPosition = initialPosition + new Vector3(0f, verticalMovement, 0f);
        transform.position = newPosition;
    }
}
