using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;   // The player's movement speed
    public float jumpForce = 10f;   // The force applied to the player when jumping
    public float wallJumpForce = 15f;   // The force applied to the player when wall jumping
    public float wallSlideSpeed = 2f;   // The speed at which the player slides down a wall
    public float wallRunSpeed = 15f;   // The speed at which the player runs along a wall
    public float wallRunDuration = 3f;   // The duration of the wall run
    public float wallRunCooldown = 5f;   // The cooldown between wall runs

    private bool isGrounded = false;   // True if the player is touching the ground
    private bool isTouchingWall = false;   // True if the player is touching a wall
    private bool isWallRunning = false;   // True if the player is currently wall running
    private bool canWallRun = true;   // True if the player is able to wall run (cooldown has passed)

    private Rigidbody rb;   // The player's rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get the player's input
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        // Calculate the player's movement vector
        Vector3 moveDir = new Vector3(xInput, 0, yInput).normalized;

        // Apply the movement vector
        rb.velocity = moveDir * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // Jump if the player is grounded and the jump button is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        // Wall jump if the player is touching a wall and the jump button is pressed
        if (isTouchingWall && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(-Mathf.Sign(xInput) * wallJumpForce, jumpForce, 0), ForceMode.Impulse);
        }

        // Wall run if the player is touching a wall and the wall run button is pressed and the player is able to wall run
        if (isTouchingWall && Input.GetButtonDown("WallRun") && canWallRun)
        {
            isWallRunning = true;
            canWallRun = false;
            StartCoroutine(WallRunTimer());
        }

        // If the player is wall running, apply the wall run speed and adjust the player's y velocity to match the wall slide speed
        if (isWallRunning)
        {
            rb.velocity = new Vector3(Mathf.Sign(xInput) * wallRunSpeed, -wallSlideSpeed, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player is touching the ground or a wall
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
            }
            else if (contact.normal.x != 0 || contact.normal.z != 0)
            {
                isTouchingWall = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the player is no longer touching the ground or a wall
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = false;
            }
            else if (contact.normal.x != 0 || contact.normal.z != 0)
            {
                isTouchingWall = false;
            }
        }
    }

    IEnumerator WallRunTimer()
    {
        // Wait for the wall run duration and then reset the wall run variables
        yield return new WaitForSeconds(wallRunDuration);
        isWallRunning = false;
        StartCoroutine(WallRunCooldownTimer());
    }

    IEnumerator WallRunCooldownTimer()
    {
        // Wait for the wall run cooldown and then enable wall running
        yield return new WaitForSeconds(wallRunCooldown);
        canWallRun = true;
    }
}
