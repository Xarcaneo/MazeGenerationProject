using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the player's movement and interactions with the game environment.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Size of each grid cell.
    /// </summary>
    [SerializeField, Tooltip("Size of each grid cell")]
    private float gridSize = 1f;

    /// <summary>
    /// Speed at which the player moves between grid cells.
    /// </summary>
    [SerializeField, Tooltip("Speed at which the player moves between grid cells.")]
    private float moveSpeed = 5f;

    /// <summary>
    /// LayerMask to define which layers are considered obstacles.
    /// </summary>
    [SerializeField, Tooltip("LayerMask to define which layers are considered obstacles.")]
    private LayerMask obstacleLayer;

    /// <summary>
    /// The target position for the player's movement.
    /// </summary>
    private Vector2 targetPosition;

    /// <summary>
    /// Indicates if the player is currently moving.
    /// </summary>
    private bool isMoving = false;

    /// <summary>
    /// Reference to the player's animation controller.
    /// </summary>
    private PlayerAnimationController animationController;

    /// <summary>
    /// Initializes the player controller.
    /// </summary>
    private void Start()
    {
        // Set the initial target position to the player's current position
        targetPosition = transform.position;

        // Get the reference to the PlayerAnimationController component
        animationController = GetComponent<PlayerAnimationController>();
    }

    /// <summary>
    /// Updates the player's position and handles input for movement.
    /// </summary>
    private void Update()
    {
        // Check if the player is not currently moving
        if (!isMoving)
        {
            // Determine the movement direction based on input
            Vector2 moveDirection = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.UpArrow))
                moveDirection = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                moveDirection = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                moveDirection = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                moveDirection = Vector2.right;

            // If a movement direction is determined
            if (moveDirection != Vector2.zero)
            {
                // Calculate the new target position
                Vector2 newPosition = (Vector2)transform.position + moveDirection * gridSize;

                // Check if the player can move to the new position
                if (CanMoveTo(newPosition, moveDirection))
                {
                    // Set the movement animation
                    animationController.SetMovementAnimation(moveDirection);

                    // Start the coroutine to move to the new position
                    StartCoroutine(MoveToPosition(newPosition));
                }
            }
        }
    }

    /// <summary>
    /// Coroutine to move the player to the specified position.
    /// </summary>
    /// <param name="newPosition">The position to move to.</param>
    /// <returns>An IEnumerator for the coroutine.</returns>
    private IEnumerator MoveToPosition(Vector2 newPosition)
    {
        // Set the isMoving flag to true
        isMoving = true;

        // Move the player to the target position
        while ((Vector2)transform.position != newPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Update the target position and reset the isMoving flag
        targetPosition = newPosition;
        isMoving = false;

        // Set the animation to idle after moving
        animationController.SetMovementAnimation(Vector2.zero);
    }

    /// <summary>
    /// Checks if the player can move to the specified position.
    /// </summary>
    /// <param name="targetPos">The target position to move to.</param>
    /// <param name="moveDirection">The direction of the movement.</param>
    /// <returns>True if the player can move to the position, false otherwise.</returns>
    private bool CanMoveTo(Vector2 targetPos, Vector2 moveDirection)
    {
        // Cast a ray from the current position to the target position to check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, gridSize, obstacleLayer);
        return hit.collider == null;
    }
}
