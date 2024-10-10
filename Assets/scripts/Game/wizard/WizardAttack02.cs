using UnityEngine;

public class WizardAttack02State : WizardState
{
    public override void Enter(Character onwer)
    {
        base.Enter(onwer);
        Owner.Animator.Play("Attack02");
    }

}