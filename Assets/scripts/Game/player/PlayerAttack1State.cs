using System;
using UnityEngine;

public class PlayerAttack1State : PlayerState
{
    private Action onPlayerAttack;
    public override void Enter(Character onwer)
    {
       
        base.Enter(onwer);
        Owner.Animator.Play("SwordmanAttack01");

    }

}