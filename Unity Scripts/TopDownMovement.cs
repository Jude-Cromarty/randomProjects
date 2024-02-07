using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Animator animator;

    void Update()
    {
        //Lock player in camera bounds

    Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); //set world space to camera bounds
    
    pos.x = Mathf.Clamp01(pos.x);
    pos.y = Mathf.Clamp01(pos.y); //clamp between 0 - 1 for movement

    transform.position = Camera.main.ViewportToWorldPoint(pos); //set camera bounds to world space

        Vector3 movement = Vector3.zero;
        // Handle keyboard input 

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            movement = new Vector3(horizontalInput, verticalInput, 0f);//get inputs on axis via input sysytem
        

        transform.position += movement * moveSpeed ;
        //links to animator for animations based on floats and trees
    animator.SetFloat("Horizontal", movement.x);
    animator.SetFloat("Vertical", movement.y);
    animator.SetFloat("Speed", movement.sqrMagnitude);
        

    }
    

}
