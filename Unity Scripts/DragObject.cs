using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;
    private Rigidbody rb;
    private bool isThrowing = false;
    private float throwTime = 2f;
    private float throwForce = 1000f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
        rb.isKinematic = true;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (!isThrowing)
        {
            rb.isKinematic = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDragging && !isThrowing)
        {
            rb.isKinematic = false;
            ThrowObject();
        }

        if (isThrowing)
        {
            throwTime += Time.deltaTime;
            if (throwTime >= 2f)
            {
                isThrowing = false;
            }
        }
    }

    private void ThrowObject()
    {
        isDragging = false;
        isThrowing = true;
        rb.AddForce(Camera.main.transform.forward * throwForce);
        throwTime = 0f;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void SetThrowForce(float newThrowForce)
    {
        throwForce = newThrowForce;
    }
}
