using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotButton : MonoBehaviour
{
    public GameObject panel; // Reference to the panel that takes the screenshot
    public string fileNamePrefix = "Fairies"; // Prefix for the screenshot file name
    public Image[] screenshotImages; // Array of Image objects to display the screenshots
    public float delayTime = 1.0f; // Time to wait before taking the screenshot

    private Texture2D[] screenshots; // Array of Texture2D objects to hold the screenshots

    // Attach this method to the button's onClick event in the Inspector
    public void TakeScreenshot()
    {
        StartCoroutine(TakeScreenshotCoroutine());
    }

    // Coroutine to take the screenshot
    private IEnumerator TakeScreenshotCoroutine()
    {
        // Hide the panel so it doesn't appear in the screenshot
        panel.SetActive(false);

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Take the screenshot
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame(); // Wait for end of frame to ensure frame is fully drawn
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Show the panel again
        panel.SetActive(true);

        // Save the screenshot to the screenshots array
        AddScreenshot(screenshot);
    }

    // Add the screenshot to the screenshots array
    private void AddScreenshot(Texture2D screenshot)
    {
        // Create the array if it doesn't exist
        if (screenshots == null)
        {
            screenshots = new Texture2D[screenshotImages.Length];
        }

        // Add the screenshot to the next available slot in the array
        for (int i = 0; i < screenshots.Length; i++)
        {
            if (screenshots[i] == null)
            {
                screenshots[i] = screenshot;
                UpdateScreenshotImage(i);
                break;
            }
        }
    }

    // Update the specified screenshot image with the corresponding screenshot from the screenshots array
private void UpdateScreenshotImage(int index)
{
    if (screenshots[index] != null && screenshotImages[index] != null)
    {
        float aspectRatio = (float)screenshots[index].width / (float)screenshots[index].height;
        screenshotImages[index].sprite = Sprite.Create(screenshots[index], new Rect(0, 0, screenshots[index].width, screenshots[index].height), new Vector2(0.5f, 0.5f), 100.0f, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), false);
        screenshotImages[index].GetComponent<RectTransform>().sizeDelta = new Vector2(500.0f, 500.0f / aspectRatio);
    }
}

}
