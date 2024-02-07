using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float initialMaxSpeed = 5f; // Initial maximum speed the object can reach.
    public float maxSpeedIncreaseRate = 0.1f; // Custom rate at which the max speed increases per second.

    private float currentMaxSpeed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMaxSpeed = initialMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase the max speed over time.
        currentMaxSpeed += maxSpeedIncreaseRate * Time.deltaTime;

        // Clamp the velocity magnitude to the current max speed.
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, currentMaxSpeed);
    }
}
