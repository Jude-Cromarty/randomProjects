using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


[RequireComponent(typeof(Image))]
public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;
    private Text spDisplay;
private GameObject SPDisplay;
private GameObject FlipButton;
static float SP = 100;
public int CardCost;
private Slider SPslider;
private GameObject SPbar;

public float Weight;
    public static float IncreasePerSecond = 1; // remove this every second
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(SP < CardCost){return;}
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
       m_DraggingIcon = new GameObject(this.gameObject.name) ;


        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
         if(SP < CardCost){return;}
        if (m_DraggingIcon != null)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
         if(SP < CardCost){return;}
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

public void OnEndDrag(PointerEventData eventData)
{
    if (SP < CardCost) { return; }

    if (m_DraggingIcon != null)
    {
        Destroy(m_DraggingIcon);

        // Raycast to find the UI element underneath the icon
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = eventData.position;

        // Perform the raycast
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        if (results.Count > 0)
        {
            GameObject droppedOnObject = results[0].gameObject;

            Debug.Log("You Played "+ gameObject.name.Replace("Card", "").Replace("(Clone)", "") + " on " + droppedOnObject.name.Replace("Card", "").Replace("(Clone)", "") );

            // Attach the script to the dropped-on object
            DroppedObjectHandler handler = droppedOnObject.GetComponent<DroppedObjectHandler>();

            if (handler != null)
            {
                if(GameObject.Find("Flip").GetComponent<Fliper>().Flipped == true){
                handler.isFlipped = true;}
                // Call the method to handle the dropped object
                handler.HandleDroppedObject(this.gameObject.name);
            }
        }

        UpdateSP();
    }


}

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
void UpdateSP()
{
     SP = Mathf.Max(0, SP - CardCost); // Ensure SP doesn't go below 0
     SPslider.value = SP;
     Destroy(gameObject);
}

void Start()
{
        SPDisplay = GameObject.Find("SPDisplay");   
        spDisplay = SPDisplay.GetComponent<Text>();
        SPbar = GameObject.Find("SPBar");
        SPslider = SPbar.GetComponent<Slider>();
        SPslider.value = 100;
}
Graphic m_Graphic;
    Color m_MyColor;
void Update()
{
    if(SP < CardCost)
    {
        m_Graphic = GetComponent<Graphic>();
        //Create a new Color that starts as red
        m_MyColor = Color.grey;
        //Change the Graphic Color to the new Color
        m_Graphic.color = m_MyColor;
    }
    if(SP > CardCost)
    {
        m_Graphic = GetComponent<Graphic>();
        //Create a new Color that starts as red
        m_MyColor = Color.white;
        //Change the Graphic Color to the new Color
        m_Graphic.color = m_MyColor;
    }
    if(SP < 100)
    {
    SP += IncreasePerSecond * Time.deltaTime;
    SPslider.value = SP;
    
    }
    

    spDisplay.text = SP.ToString("F0") + "/100";

}

}