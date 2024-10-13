using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float frictionFactor = 10f;
    private float originalFriction; // Armazena o valor original
    private float lastInput = 0f;

    public override void Enter(Character owner)
    {
        base.Enter(owner);
        originalFriction = Owner.Friction; // Salva o atrito original
        Owner.Bounce(new Vector2(0,Owner.JumpForce));
        Owner.Friction = originalFriction / frictionFactor; // Diminui o atrito enquanto está no ar
        lastInput = Owner.Body.velocity.normalized.x;
        Owner.Animator.Play("SwordmanJump");
    }

    public override void FixedUpdate()
    {
        Owner.AddInput(lastInput); 
    }


    public override void Exit()
    {
        base.Exit();
        Owner.Friction = originalFriction; // Restaura o atrito ao sair do estado
    }
}
