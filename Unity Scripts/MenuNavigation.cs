using UnityEngine;
using System.Collections.Generic;

public class MenuNavigation : MonoBehaviour
{
    public List<GameObject> centerObjects;
    private int currentIndex = 0;

    public void SwitchToNextCenterObject()
    {
        currentIndex++;

        if (currentIndex >= centerObjects.Count)
        {
            currentIndex = 0;
        }

        ActivateCurrentCenterObject();
    }

    public void SwitchToPreviousCenterObject()
    {
        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = centerObjects.Count - 1;
        }

        ActivateCurrentCenterObject();
    }

    private void ActivateCurrentCenterObject()
    {
        for (int i = 0; i < centerObjects.Count; i++)
        {
            centerObjects[i].SetActive(i == currentIndex);
        }
    }
}
