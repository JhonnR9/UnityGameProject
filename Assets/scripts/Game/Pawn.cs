using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D))]
public class Pawn : Actor, IPawn
{
    protected Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero; // Velocidade atual
    private Vector2 inputDirection = Vector2.zero; // Armazena o input

    [SerializeField] private float maxSpeed = 5.0f; // Velocidade máxima
    [SerializeField] private float acceleration = 2.0f; // Aceleração ao pressionar as teclas
    [SerializeField] private float friction = 0.9f; // Atrito quando não há input
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

    public virtual void FixedUpdate()
    {
        // Atualiza a velocidade baseada na direção do input
        if (inputDirection != Vector2.zero)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, inputDirection.x * maxSpeed, acceleration);
            velocity.y = Mathf.MoveTowards(velocity.y, inputDirection.y * maxSpeed, acceleration);
        }
        else
        {
            // Aplica atrito quando não há input (com suavização)
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction);
        }

        // Limita a velocidade máxima
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);

        // Movimenta o Rigidbody2D
        rb.velocity = new Vector2(velocity.x, rb.velocity.y); // Mantemos a componente Y para gravidade/impulso
        inputDirection = Vector2.zero; // Reseta a direção de input
    }

    // Função que recebe um Vector2 como input
    public void AddInput(Vector2 input)
    {
        inputDirection += input;
        inputDirection.x = Mathf.Clamp(inputDirection.x, -1, 1); // Limita a direção horizontal entre -1 e 1
        inputDirection.y = Mathf.Clamp(inputDirection.y, -1, 1); // Limita a direção vertical entre -1 e 1
    }

    // Função antiga que recebe apenas um float (input horizontal)
    public void AddInput(float horizontalInput)
    {
        inputDirection.x += horizontalInput;
        inputDirection.x = Mathf.Clamp(inputDirection.x, -1, 1); // Limita a direção horizontal entre -1 e 1
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
