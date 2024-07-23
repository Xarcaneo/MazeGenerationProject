using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's animations based on movement direction.
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    /// <summary>
    /// Reference to the Animator component.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Stores the last direction in which the player moved.
    /// </summary>
    private Vector2 lastMoveDirection;

    /// <summary>
    /// Initializes the Animator component reference.
    /// </summary>
    private void Awake()
    {
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Sets the movement animation based on the provided direction.
    /// </summary>
    /// <param name="moveDirection">The direction in which the player is moving.</param>
    public void SetMovementAnimation(Vector2 moveDirection)
    {
        // Update the last move direction if the player is moving
        if (moveDirection != Vector2.zero)
            lastMoveDirection = moveDirection;

        // Set the appropriate animation based on the move direction
        switch (moveDirection)
        {
            case Vector2 up when moveDirection == Vector2.up:
                animator.Play("MoveUp");
                break;
            case Vector2 down when moveDirection == Vector2.down:
                animator.Play("MoveDown");
                break;
            case Vector2 left when moveDirection == Vector2.left:
                animator.Play("MoveSide");
                transform.localScale = new Vector3(-1, 1, 1); // Flip sprite to face left
                break;
            case Vector2 right when moveDirection == Vector2.right:
                animator.Play("MoveSide");
                transform.localScale = new Vector3(1, 1, 1); // Set sprite to default (right-facing)
                break;
            default:
                SetIdleAnimation();
                break;
        }
    }

    /// <summary>
    /// Sets the idle animation based on the last move direction.
    /// </summary>
    private void SetIdleAnimation()
    {
        // Determine which idle animation to play based on the last move direction
        switch (lastMoveDirection)
        {
            case Vector2 up when lastMoveDirection == Vector2.up:
                animator.Play("IdleUp");
                break;
            case Vector2 down when lastMoveDirection == Vector2.down:
                animator.Play("IdleDown");
                break;
            case Vector2 left when lastMoveDirection == Vector2.left:
                animator.Play("IdleSide");
                transform.localScale = new Vector3(-1, 1, 1); // Ensure sprite faces left
                break;
            case Vector2 right when lastMoveDirection == Vector2.right:
                animator.Play("IdleSide");
                transform.localScale = new Vector3(1, 1, 1); // Ensure sprite faces right
                break;
        }
    }
}
