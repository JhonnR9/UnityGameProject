using System;
using UnityEngine;

public class PlayerAttack1State : PlayerState
{
    private Action onPlayerAttack;

    private BoxCollider2D hitBox;
    public override void Enter(Character onwer)
    {
        base.Enter(onwer);
        Owner.Animator.Play("SwordmanAttack01");

        if (hitBox == null)
        {
            var colliders = Owner.GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D boxCollider2D in colliders)
            {
                if (boxCollider2D.isTrigger)
                {
                    hitBox = boxCollider2D;
                }
            }
        }

        hitBox.enabled = true;

    }
    public override void Exit()
    {
        base.Exit();
        hitBox.enabled=false;
    }
}