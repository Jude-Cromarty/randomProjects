using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private float snapSpeed = 10f;

    private Coroutine scrollingCoroutine;

    private void Start()
    {
        scrollRect.inertia = true;
    }

    public void SnapToNearestPanel()
    {
        if (scrollingCoroutine != null)
            StopCoroutine(scrollingCoroutine);

        float targetNormalizedPosX = FindClosestNormalizedPosition();
        scrollingCoroutine = StartCoroutine(SmoothScrollTo(targetNormalizedPosX));
    }

    private float FindClosestNormalizedPosition()
    {
        float normalizedPosition = 0f;
        float minDistance = float.MaxValue;

        foreach (RectTransform panel in contentRect)
        {
            float distance = Mathf.Abs(GetDistanceToCenter(panel));
            if (distance < minDistance)
            {
                minDistance = distance;
                normalizedPosition = GetNormalizedPosition(panel);
            }
        }

        return normalizedPosition;
    }

    private IEnumerator SmoothScrollTo(float targetNormalizedPosX)
    {
        float currentNormalizedPosX = scrollRect.horizontalNormalizedPosition;

        while (Mathf.Abs(currentNormalizedPosX - targetNormalizedPosX) > 0.001f)
        {
            currentNormalizedPosX = Mathf.Lerp(currentNormalizedPosX, targetNormalizedPosX, snapSpeed * Time.deltaTime);
            scrollRect.horizontalNormalizedPosition = currentNormalizedPosX;
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetNormalizedPosX;
    }

    private float GetDistanceToCenter(RectTransform target)
    {
        Vector3[] targetCorners = new Vector3[4];
        target.GetWorldCorners(targetCorners);

        Vector3[] viewportCorners = new Vector3[4];
        scrollRect.viewport.GetWorldCorners(viewportCorners);

        Vector3 centerTarget = (targetCorners[2] + targetCorners[3]) * 0.5f;
        Vector3 centerViewport = (viewportCorners[2] + viewportCorners[3]) * 0.5f;

        return centerTarget.x - centerViewport.x;
    }

    private float GetNormalizedPosition(RectTransform target)
    {
        float targetPosX = target.anchoredPosition.x;
        float contentWidth = contentRect.rect.width;
        float viewportWidth = scrollRect.viewport.rect.width;

        return (targetPosX + contentWidth * 0.5f) / (contentWidth - viewportWidth);
    }
}
