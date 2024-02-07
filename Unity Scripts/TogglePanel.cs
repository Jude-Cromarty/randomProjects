using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public List<GameObject> panels; // The UI panels to show/hide
    private static int currentPanelIndex = 0; // The index of the currently active panel

    // Use this method to show the current panel and hide the others
    public void ShowPanel()
    {
        // Set the current panel active and hide the others
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == currentPanelIndex)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    // Use this method to toggle the visibility of the current panel
    public void ToggleVisibility()
    {
        // Toggle the visibility of the current panel
        bool isPanelActive = panels[currentPanelIndex].activeSelf;
        panels[currentPanelIndex].SetActive(!isPanelActive);
    }

    // Use this method to switch to the next panel and show it
public void NextPanel()
{
    // Hide the current panel
    panels[currentPanelIndex].SetActive(false);

    // Move to the next panel index, wrapping around if necessary
    currentPanelIndex = (currentPanelIndex + 1) % panels.Count;

    // Show the new current panel
    panels[currentPanelIndex].SetActive(true);
}

    // Use this method to switch to the previous panel and show it
    public void PreviousPanel()
    {
        // Hide the current panel
        panels[currentPanelIndex].SetActive(false);

        // Move to the previous panel index, wrapping around if necessary
        currentPanelIndex--;
        if (currentPanelIndex < 0)
        {
            currentPanelIndex = panels.Count - 1;
        }

        // Show the new current panel
        panels[currentPanelIndex].SetActive(true);
    }
}
