using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Size of each grid cell")]
    private float gridSize = 1f;

    [SerializeField, Tooltip("Speed at which the player moves between grid cells.")]
    private float moveSpeed = 5f;

    [SerializeField, Tooltip("LayerMask to define which layers are considered obstacles.")]
    private LayerMask obstacleLayer;

    private Vector2 targetPosition;
    private bool isMoving = false;

    private PlayerAnimationController animationController;

    private void Start()
    {
        targetPosition = transform.position;
        animationController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            Vector2 moveDirection = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.UpArrow))
                moveDirection = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                moveDirection = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                moveDirection = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                moveDirection = Vector2.right;

            if (moveDirection != Vector2.zero)
            {
                Vector2 newPosition = (Vector2)transform.position + moveDirection * gridSize;

                if (CanMoveTo(newPosition, moveDirection))
                {
                    animationController.SetMovementAnimation(moveDirection);

                    StartCoroutine(MoveToPosition(newPosition));
                }
            }
        }
    }

    private IEnumerator MoveToPosition(Vector2 newPosition)
    {
        isMoving = true;
        while ((Vector2)transform.position != newPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        targetPosition = newPosition;
        isMoving = false;
        animationController.SetMovementAnimation(Vector2.zero); // Set to idle after moving
    }

    private bool CanMoveTo(Vector2 targetPos, Vector2 moveDirection)
    {
        // Cast a ray from the current position to the target position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, gridSize, obstacleLayer);
        return hit.collider == null;
    }
}
