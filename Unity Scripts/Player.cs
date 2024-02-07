using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public float moveSpeed, jumpForce, defaultSpeed;
    public float sprintSpeed;
    private Rigidbody rb;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    // Start is called before the first frame update

    void Start()
    {
       rb = GetComponent<Rigidbody>();
	} 
    

    // Update is called once per frame
    void Update()
    {
//Backwards & Forwards
    Vector3 movement = new Vector3(Input.GetAxis("Vertical"), 0.0f);
    transform.position -= movement * Time.deltaTime * moveSpeed;      
//Rotate(Tilting)
    float tiltAroundX = Input.GetAxis("Horizontal") * tiltAngle;
    float tiltAroundZ = 0 * tiltAngle;
    Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
 //Jump
    if(Input.GetKey(KeyCode.Space)){rb.velocity = Vector3.up * jumpForce;}
//Sprint
    moveSpeed = defaultSpeed;
    if(Input.GetKey(KeyCode.LeftShift)){moveSpeed = sprintSpeed;}
    }

}

