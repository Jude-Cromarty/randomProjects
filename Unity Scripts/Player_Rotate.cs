using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f; // Adjust this value to control the rotation speed

    private void Update()
    {
        // Get the horizontal and vertical inputs (arrow keys)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Check if any arrow key is pressed
        if (horizontalInput != 0 || verticalInput != 0)
        {
            // Calculate the angle based on the arrow keys pressed
            float targetAngle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;

            // Smoothly rotate the object towards the target angle
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, targetAngle), step);
        }
    }
}
