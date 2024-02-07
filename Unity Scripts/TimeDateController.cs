using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeDateController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;
    public Image imageUI;

    // Define an array of pastel color hex codes.
private string[] pastelColors = {
    "#F0E68C", // Khaki (Light Yellow)
    "#FFB6C1", // Light Pink
    "#98FB98", // Pale Green
    "#87CEEB", // Sky Blue
    "#FFA07A", // Light Salmon
    "#9370DB"  // Medium Purple (Darker Color)
};


    private void Start()
    {
        // Check if TextMeshPro, Image, and pastelColors are assigned.
        if (timeText == null || dateText == null || imageUI == null || pastelColors.Length == 0)
        {
            Debug.LogError("TextMeshPro, Image, or pastelColors not assigned!");
            return;
        }

        // Set the initial image color.
        UpdateImageColor(DateTime.Now);
    }

    private void Update()
    {
        // Get the current time.
        DateTime now = DateTime.Now;

        // Update the TextMeshPro components.
        timeText.text = now.ToString("HH:mm:ss");
        dateText.text = now.ToString("yyyy-MM-dd");

        // Update the image color.
        UpdateImageColor(now);
    }

    private void UpdateImageColor(DateTime now)
    {
        // Calculate the normalized time (0.0 to 1.0) in a day.
        float normalizedTime = (float)(now.Hour * 3600 + now.Minute * 60 + now.Second) / 86400f;

        // Calculate the index of the pastel color based on the normalized time.
        int colorIndex = Mathf.FloorToInt(normalizedTime * (pastelColors.Length - 1));

        // Get the hex code for the current pastel color.
        string hexColor = pastelColors[colorIndex];

        // Convert the hex code to a Color.
        Color color = HexToColor(hexColor);

        // Apply the color to the UI Image.
        imageUI.color = color;
    }

    // Helper method to convert hex color code to Color.
    private Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
