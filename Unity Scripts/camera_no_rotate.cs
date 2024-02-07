using UnityEngine;

public class camera_no_rotate : MonoBehaviour
{
    public Transform player; // Reference to the player's transform.

    // Start is called before the first frame update
    void Start()
    {
    }

    // LateUpdate is called once per frame, after all Update functions have been called.
    void LateUpdate()
    {
        // Update the camera's position to match the player's position on the y-axis, keeping the original x and z positions.
        Vector3 newPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
