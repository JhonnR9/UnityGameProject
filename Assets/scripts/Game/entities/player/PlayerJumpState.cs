using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float lastInput = 0f;

    public override void Enter(Character owner)
    {
        base.Enter(owner);

        Owner.Bounce(new Vector2(0,Owner.JumpForce));
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
    }
}
