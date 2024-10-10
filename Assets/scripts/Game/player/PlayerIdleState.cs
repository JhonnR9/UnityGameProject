using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void Enter(Character onwer)
    {
        base.Enter(onwer);
        Owner.Animator.Play("SwordmanIdle");
    }

}