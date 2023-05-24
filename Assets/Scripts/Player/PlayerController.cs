using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), (typeof(PlayerHealth)))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float jumpSpeed = 4f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float airWalkSpeed = 3f;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    
    private TouchingDirections touchingDirections;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerHealth playerHealth;
    
    private bool _isMoving = false;
    private bool _isJumping = false;
    public bool _IsFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerHealth = GetComponent<PlayerHealth>();
        coyoteTimeCounter = coyoteTime;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsOnWall || !playerHealth.IsAlive) return;
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y); 
        animator.SetFloat(AnimatorTags.yVelocity, rb.velocity.y);
    }

    private bool IsJumping
    {
        get => _isJumping;
        set
        {
            _isJumping = value;
            animator.SetBool(AnimatorTags.isJumping, value);
        }
    }

    private bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            animator.SetBool(AnimatorTags.isMoving, value);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (PauseMenuUI.isPaused) return;
        moveInput = context.ReadValue<Vector2>();
        if (playerHealth.IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        } else
            IsMoving = false;

    }

    private float CurrentMoveSpeed
    {
        get
        {
            bool isMoving = IsMoving;
            bool isOnWall = touchingDirections.IsOnWall;
            bool isGrounded = touchingDirections.IsGrounded;
            if (isMoving && !isOnWall)
            {
                return isGrounded ? walkSpeed : airWalkSpeed;
            }
            else
            {
                return 0;
            }
        }
    }

    private bool IsFacingRight
    {
        get => _IsFacingRight;
        set
        {
            if (_IsFacingRight != value)
            {
                //using Scale instead of Rotation for setting up for child directional based actions to display same as the character flips
                transform.localScale *= new Vector2(-1, 1);
            }
            _IsFacingRight = value;
        }
    }

    private void SetFacingDirection(Vector2 movementVector2)
    {
        if (IsMoving)
        {
            IsFacingRight = movementVector2.x switch
            {
                > 0 when !IsFacingRight => true,
                < 0 when IsFacingRight => false,
                _ => IsFacingRight
            };
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (PauseMenuUI.isPaused) return;
        if (!context.started && playerHealth.IsAlive || (!touchingDirections.IsGrounded && !(coyoteTimeCounter > 0))) return;
        Jump();
        IsJumping = true;
    }

    private void Jump()
    {
        coyoteTimeCounter = 0;
        animator.SetTrigger(AnimatorTags.jump);
        AudioManager.AudioInstance.PlayClip("JumpSFX", 1f);
        rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed), ForceMode2D.Impulse);
    }

    private void Update()
    {
        switch (touchingDirections.IsGrounded)
        {
            case false when coyoteTimeCounter > 0:
                coyoteTimeCounter -= Time.deltaTime;
                break;
            case true:
                IsJumping = false;
                break;
        }

    }
}