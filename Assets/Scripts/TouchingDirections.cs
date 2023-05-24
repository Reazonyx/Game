using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),(typeof(Collider2D)))]
public class TouchingDirections : MonoBehaviour
{
    private Collider2D touchingCollider;
    private Animator animator;
    public ContactFilter2D castFilter;
    private RaycastHit2D[] touchGround = new RaycastHit2D[6]; // I could use box Box Collider 2D as a trigger instead of the Raycast 
    private RaycastHit2D[] touchWall = new RaycastHit2D[6];
    private RaycastHit2D[] touchCeiling = new RaycastHit2D[6];
    
    [SerializeField]private float groundDistance = 0.05f;
    [SerializeField]private float ceilingDistance = 0.05f;
    [SerializeField]private float wallDistance = 0.3f;

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isOnWall = false;
    [SerializeField] private bool isOnCeiling = false;

    private void Awake()
    {
        touchingCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, touchGround, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(WallCheckDirection, castFilter, touchWall, wallDistance) > 0;
        IsOnWall = touchingCollider.Cast(Vector2.up, castFilter, touchCeiling, ceilingDistance) > 0;
    }

    private Vector2 WallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded
    {
        get => isGrounded;
        private set
        {
            isGrounded = value;
            animator.SetBool(AnimatorTags.isGrounded, value);
        }
    }
    public bool IsOnWall
    {
        get => isOnWall;
        private set
        {
            isOnWall = value;
            animator.SetBool(AnimatorTags.isOnWall, value);
        }
    }
    
    public bool IsOnCeiling
    {
        get => isOnCeiling;
        private set
        {
            isOnCeiling = value;
            animator.SetBool(AnimatorTags.isOnCeiling, value);
        }
    }
}
