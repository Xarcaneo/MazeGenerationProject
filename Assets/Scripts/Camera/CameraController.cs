using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the camera movement and zooming functionality in the Unity scene.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Speed at which the camera moves when dragged.
    /// </summary>
    [SerializeField, Tooltip("Speed at which the camera moves when dragged")]
    private float dragSpeed = 0.5f;  // Lowered the drag speed for finer control

    /// <summary>
    /// Speed at which the camera zooms in and out.
    /// </summary>
    [SerializeField, Tooltip("Speed at which the camera zooms in and out")]
    private float zoomSpeed = 2f;

    /// <summary>
    /// Minimum orthographic size of the camera for zoom.
    /// </summary>
    [SerializeField, Tooltip("Minimum orthographic size of the camera for zoom")]
    private float minZoom = 2f;

    /// <summary>
    /// Maximum orthographic size of the camera for zoom.
    /// </summary>
    [SerializeField, Tooltip("Maximum orthographic size of the camera for zoom")]
    private float maxZoom = 10f;

    // Stores the origin point of the drag
    private Vector3 dragOrigin;

    /// <summary>
    /// Updates the camera position and zoom based on user input.
    /// </summary>
    void Update()
    {
        // Handle camera dragging when the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Capture the initial mouse position when the drag starts
            dragOrigin = Input.mousePosition;
            return;
        }

        // Continue dragging the camera if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            // Adjust drag speed based on current zoom level for smoother control
            float adjustedDragSpeed = dragSpeed * Camera.main.orthographicSize;
            // Calculate the new position based on the difference between the current and initial mouse positions
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * adjustedDragSpeed, pos.y * adjustedDragSpeed, 0);

            // Translate the camera position by the calculated move vector
            transform.Translate(move, Space.World);
        }

        // Handle camera zooming with the mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // Adjust the camera's orthographic size within the specified min and max zoom limits
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
    }
}
