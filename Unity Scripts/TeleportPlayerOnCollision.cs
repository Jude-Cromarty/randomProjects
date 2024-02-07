using UnityEngine;

public class TeleportPlayerOnCollision : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player object
    public Vector3 teleportPosition = Vector3.zero; // The position to which the player will be teleported

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the specified playerTag
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Teleport the player to the specified position
            collision.gameObject.transform.position = teleportPosition;
        }
    }
}
