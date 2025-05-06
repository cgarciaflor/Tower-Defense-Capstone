using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIVisibilityChecker : MonoBehaviour
{
    public Transform worldUIElement;
    public TMPro.TextMeshProUGUI fallbackText; 
    public Camera mainCamera;

    public float screenEdgeBuffer = 50f; 

    void Update()
    {
        if (worldUIElement == null || fallbackText == null || mainCamera == null)
            return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldUIElement.position);

        // If behind camera
        if (screenPos.z < 0)
        {
            fallbackText.enabled = true;
            return;
        }

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        bool isOffScreen =
            screenPos.x < -screenEdgeBuffer ||
            screenPos.x > screenWidth + screenEdgeBuffer ||
            screenPos.y < -screenEdgeBuffer ||
            screenPos.y > screenHeight + screenEdgeBuffer;

        fallbackText.enabled = isOffScreen;
    }
}
