using UnityEngine;
using UnityEngine.UI;

public class ButtonImageInstantiator : MonoBehaviour
{
    public GameObject imagePrefab; // the prefab to instantiate
    private Transform canvasTransform; // the canvas transform to add the image to
    private Sprite imageSprite; // the sprite to use for the instantiated image

    void Start()
    {
        // make sure the canvas transform is not null
        if (canvasTransform == null)
        {
            canvasTransform = GameObject.Find("Canvas").transform;
        }
    }

   public void OnButtonClick()
    {
        // get the position and size of the button
        Vector3 buttonPos = transform.position;
        RectTransform buttonRect = GetComponent<RectTransform>();
        Vector2 buttonSize = buttonRect.sizeDelta;
        Vector2 buttonAnchorPos = buttonRect.anchoredPosition;

        // instantiate the image prefab and set its position and size to match the button
        GameObject imageObj = Instantiate(imagePrefab, canvasTransform);
        RectTransform imageRect = imageObj.GetComponent<RectTransform>();
        imageRect.sizeDelta = buttonSize;
        imageRect.anchoredPosition = buttonAnchorPos;

        // set the sprite of the image (assuming it has an Image component)
        Image image = imageObj.GetComponent<Image>();
        if (image != null && imageSprite != null)
        {
            image.sprite = imageSprite;
        }

        // make the button inactive if the image was created successfully
        if (imageObj != null)
        {
            gameObject.SetActive(false);
        }
    }
}