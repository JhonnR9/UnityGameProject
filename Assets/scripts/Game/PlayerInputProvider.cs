using UnityEngine;

public class PlayerInputProvider
{
    private float horizontalAxis;
    private bool isJumping;
    private bool isAttacking;

    public float HorizontalAxis => horizontalAxis;
    public bool IsJumping =>isJumping;
    public bool IsAttacking => isAttacking;

    public void Update ()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        isJumping = Input.GetButton("Jump");
        isAttacking = Input.GetButton("Fire1");
    }
}