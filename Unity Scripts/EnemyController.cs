using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10.0f;
    private Transform player;
    private Rigidbody rb;
    private SelfBalancingController selfBalancingController;
    private Transform visualTransform;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        selfBalancingController = GetComponent<SelfBalancingController>();
        visualTransform = transform.GetChild(0); // Assuming the visual object is the first child of the enemy
    }

    void Update()
    {
        // Rotate the visual object towards the player
        Vector3 direction = player.position - visualTransform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        visualTransform.rotation = Quaternion.Slerp(visualTransform.rotation, lookRotation, Time.deltaTime * speed);

        // Set the target angle for the self-balancing controller based on the tilt of the enemy
        float tiltAngle = Mathf.Abs(transform.eulerAngles.z);
        float targetAngle = Mathf.Clamp(-tiltAngle, -30f, 30f); // Limit the tilt angle to -30 to 30 degrees
        selfBalancingController.targetAngle = targetAngle;
    }
}
