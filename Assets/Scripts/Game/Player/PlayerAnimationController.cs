using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastMoveDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMovementAnimation(Vector2 moveDirection)
    {
        if(moveDirection != Vector2.zero)
            lastMoveDirection = moveDirection;

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

    private void SetIdleAnimation()
    {
        // Logic to determine which idle animation to play
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
