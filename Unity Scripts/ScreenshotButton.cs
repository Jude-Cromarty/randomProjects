using UnityEngine;
using UnityEngine.UI;

public class ScreenshotButton : MonoBehaviour
{
    public GameObject panel; // Reference to the panel that takes the screenshot
    public string fileNamePrefix = "Fairies"; // Prefix for the screenshot file name

    // Attach this method to the button's onClick event in the Inspector
    public void TakeScreenshot()
    {
        // Hide the panel so it doesn't appear in the screenshot
        panel.SetActive(false);

        // Take the screenshot
        string fileName = fileNamePrefix + "_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
        ScreenCapture.CaptureScreenshot(fileName);

        // Show the panel again
        panel.SetActive(true);
    }
}
