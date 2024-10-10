using UnityEngine;

public class WizardIdleState : WizardState
{
    public override void Enter(Character onwer)
    {
        base.Enter(onwer);
        Owner.Animator.Play("WizardIdle");
    }

}