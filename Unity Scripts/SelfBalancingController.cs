using UnityEngine;

public class SelfBalancingController : MonoBehaviour
{
    public float targetAngle = 0f; // The desired angle for the object to balance at
    public float kp = 10f; // Proportional gain
    public float ki = 0f; // Integral gain
    public float kd = 5f; // Derivative gain
    public float maxSpeed = 50f; // Maximum speed of rotation
    public float maxTorque = 100f; // Maximum torque applied to rotate the object

    private float error = 0f;
    private float integral = 0f;
    private float derivative = 0f;
    private float previousError = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        error = targetAngle - transform.eulerAngles.x;
        integral += error * Time.fixedDeltaTime;
        derivative = (error - previousError) / Time.fixedDeltaTime;
        float correction = (kp * error) + (ki * integral) + (kd * derivative);
        float torque = Mathf.Clamp(correction, -maxTorque, maxTorque);
        rb.AddTorque(Vector3.right * torque);

        previousError = error;
    }
}
