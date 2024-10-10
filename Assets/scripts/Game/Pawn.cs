using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D))]
public class Pawn : MonoBehaviour, IPawn
{
    protected Rigidbody2D rb;
    private float direction = 0; // Direção que o jogador está pressionando
    private Vector2 velocity = Vector2.zero; // Velocidade atual

    [SerializeField] private float maxSpeed = 5.0f; // Velocidade máxima
    [SerializeField] private float acceleration = 2.0f; // Aceleração ao pressionar as teclas
    [SerializeField] private float friction = 0.9f; // Atrito quando não há input
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private float slopeTolerance = 0.5f;

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; } // Corrigido
    public float Friction { get => friction; set => friction = value; }
    public float GroundCheckDistance { get => groundCheckDistance; set => groundCheckDistance = value; }
    public float SlopeTolerance { get => slopeTolerance; set => slopeTolerance = value; }
    public Rigidbody2D Body => rb;

    public bool IsGrounded { get => IsOnGround(); }

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        // Atualiza a velocidade baseada na direção do input
        if (direction != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, direction * maxSpeed, acceleration);
        }
        else
        {
            // Aplica atrito quando não há input (com suavização)
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction);
        }

        // Limita a velocidade máxima
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        // Movimenta o Rigidbody2D
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);

        direction = 0;
    }

    public void AddInput(float horizontalInput)
    {
        direction += horizontalInput;
        direction = Mathf.Clamp(direction, -1, 1); // Limita a direção entre -1 e 1
    }

    public void Bounce(float amount)
    {
        if (IsGrounded)
        {
            rb.AddForce(new Vector2(0, amount), ForceMode2D.Impulse); // Aplica uma força vertical se estiver no chão
        }
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

    public void ApplyFlip(bool canFlip)
    {
        Vector3 scale = transform.localScale;
        float originalScale = Mathf.Abs(scale.x);
        scale.x = canFlip ? originalScale : -originalScale;
        transform.localScale = scale;
    }
}
