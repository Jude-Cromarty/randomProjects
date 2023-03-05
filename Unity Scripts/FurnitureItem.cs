using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private int stock = 1; // Serialized field for the stock

    private bool draggable = true;
    private Vector3 offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            offset = transform.position - new Vector3(eventData.position.x, eventData.position.y, transform.position.z);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            Vector3 newPosition = new Vector3(eventData.position.x, eventData.position.y, transform.position.z) + offset;
            transform.position = newPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if the drop point is on the canvas
        if (eventData.pointerEnter.CompareTag("Canvas"))
        {
            // Check if there is enough stock
            if (stock > 0)
            {
                // Create a new FurnitureItem object
                GameObject newItem = Instantiate(gameObject, eventData.pointerEnter.transform);
                newItem.transform.position = eventData.pointerCurrentRaycast.worldPosition;
                newItem.GetComponent<FurnitureItem>().stock = stock - 1; // Decrease the stock of the new item
            }
        }
    }
}
