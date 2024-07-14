using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Speed at which the camera moves when dragged")]
    private float dragSpeed = 0.5f;  // Lowered the drag speed for finer control

    [SerializeField, Tooltip("Speed at which the camera zooms in and out")]
    private float zoomSpeed = 2f;

    [SerializeField, Tooltip("Minimum orthographic size of the camera for zoom")]
    private float minZoom = 2f;

    [SerializeField, Tooltip("Maximum orthographic size of the camera for zoom")]
    private float maxZoom = 10f;

    private Vector3 dragOrigin;

    void Update()
    {
        // Drag Camera
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            float adjustedDragSpeed = dragSpeed * Camera.main.orthographicSize;
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * adjustedDragSpeed, pos.y * adjustedDragSpeed, 0);

            transform.Translate(move, Space.World);
        }

        // Zoom Camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
    }
}
