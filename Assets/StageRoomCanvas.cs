using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRoomCanvas : MonoBehaviour
{
    Canvas canvas;
    void Start()
    {
        Camera mainCamera = Camera.main;

        // Get the Canvas component attached to this GameObject
        canvas = GetComponent<Canvas>();

        // If the Canvas and main camera are found, assign the main camera to the Canvas
        if (canvas != null && mainCamera != null)
        {
            canvas.worldCamera = mainCamera;
        }
        else
        {
            Debug.LogWarning("Canvas or Main Camera not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
