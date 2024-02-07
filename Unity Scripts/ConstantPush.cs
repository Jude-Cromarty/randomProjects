using UnityEngine;

public class ConstantPush : MonoBehaviour
{
    public float pushSpeed = 5.0f;

    private void Update()
    {
        // Calculate the forward direction of the GameObject
        Vector3 forwardDirection = transform.forward;

        // Apply a constant force to push the GameObject forward
        GetComponent<Rigidbody>().AddForce(forwardDirection * pushSpeed, ForceMode.Acceleration);
    }
}
