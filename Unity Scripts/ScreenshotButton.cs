using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;
using System.Collections;
using System.IO;

public class ScreenshotButton : MonoBehaviour
{
    public GameObject panel; // Reference to the panel that takes the screenshot
    public string fileNamePrefix = "Redesign"; // Prefix for the screenshot file name
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

        // Request storage permission if it is not already granted
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite));
        }

        // Take the screenshot
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame(); // Wait for end of frame to ensure frame is fully drawn
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Show the panel again
        panel.SetActive(true);

        // Save the screenshot to the appropriate directory
        string fileName = fileNamePrefix + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        string picturesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        string filePath = Path.Combine(picturesPath, fileName);
        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        if (File.Exists(filePath))
        {
            Debug.Log("Screenshot saved to: " + filePath);
        }
        else
        {
            Debug.LogError("Failed to save the screenshot at: " + filePath);
        }
    }
}
