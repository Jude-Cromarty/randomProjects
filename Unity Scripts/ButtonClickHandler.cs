using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Image fillBar;
    public float fillAmount = 0.1f;
    public Color fillColor;

    private float maxValue;

    private void Start()
    {
        maxValue = fillBar.rectTransform.rect.width;
    }

    public void OnButtonClick()
    {
        fillBar.fillAmount += fillAmount / maxValue;

        if (fillBar.fillAmount >= 1f)
        {
            fillBar.fillAmount = 0f;
            fillBar.color = fillColor;
        }
    }
}
