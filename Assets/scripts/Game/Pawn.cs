using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D))]
public class Pawn : Actor
{
    protected Rigidbody2D rb;
    private Vector2 inputDirection = Vector2.zero;

    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float friction = 0.0f; 
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private float slopeTolerance = 0.5f;

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public float Friction { get => friction; set => friction = value; }
    public float GroundCheckDistance { get => groundCheckDistance; set => groundCheckDistance = value; }
    public float SlopeTolerance { get => slopeTolerance; set => slopeTolerance = value; }
    public Rigidbody2D Body => rb;

    public bool IsGrounded { get => IsOnGround(); }

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void HandleMovement()
    {
            Vector2 newVelocity = Vector2.zero;
        if (inputDirection.magnitude > 0)
        {
            if(inputDirection.x > 0)
            {
                newVelocity.x = Mathf.MoveTowards(newVelocity.x, MaxSpeed, Acceleration);
               // newVelocity.y = inputDirection.y * -maxSpeed;
            }
            else
            {
                newVelocity.x = Mathf.MoveTowards(newVelocity.x, -MaxSpeed, Acceleration);
               // newVelocity.y = inputDirection.y * maxSpeed;
            }

        }
        else
        {
            newVelocity.x = Mathf.MoveTowards(rb.velocity.x, 0, Friction);

        }


        if (inputDirection.y == 0)
        {
            newVelocity.y = rb.velocity.y;

        }

       

        rb.velocity = newVelocity;

       // rb.velocity = new Vector2(rb.velocity.x, inputDirection.y * maxSpeed);

        inputDirection = Vector2.zero;
    }

    public virtual void FixedUpdate()
    {
        HandleMovement();
    }

    private bool IsOnGround()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = rb.Cast(Vector2.down, hits, groundCheckDistance);

        if (hitCount > 0)
        {
            RaycastHit2D hit = hits[0];
            return Vector2.Dot(hit.normal, Vector2.up) > slopeTolerance;
        }

        return false;
    }


    public void Bounce(Vector2 amount)
    {
        rb.AddForce(amount, ForceMode2D.Impulse);
    }

    public void AddInput(Vector2 input)
    {
        inputDirection += input;
        inputDirection.x = Mathf.Clamp(inputDirection.x, -1, 1); 
        inputDirection.y = Mathf.Clamp(inputDirection.y, -1, 1); 
    }

    public void AddInput(float horizontalInput)
    {
        inputDirection.x += horizontalInput;
        inputDirection.x = Mathf.Clamp(inputDirection.x, -1, 1); 
    }

    public bool IsAnimationFinished(Animator animator)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
    }

    public void ApplyFlip(bool canFlip)
    {
        Vector3 scale = transform.localScale;
        float originalScale = Mathf.Abs(scale.x);
        scale.x = canFlip ? originalScale : -originalScale;
        transform.localScale = scale;
    }
}
