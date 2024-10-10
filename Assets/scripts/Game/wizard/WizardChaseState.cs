using UnityEngine;
using Game.Events;

public class WizardChaseState : WizardState
{
    private float originalFildOfVision;
    public override void Enter(Character owner)
    {
        base.Enter(owner);
        Owner.Animator.Play("WizardRun");
        originalFildOfVision = Owner.FieldOfVision;
        Owner.FieldOfVision = Owner.ShotRange * 1.3f;


    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        RaycastHit2D[] hits = Owner.RaycastHalfCircle(Owner.FieldOfVision, Owner.RayNumberVision);
        bool playerFound = false;

        foreach (var hit in hits)
        {
            GameObject gameObject = hit.collider.gameObject;
            Player player = gameObject.GetComponent<Player>();
            if (player != null)
            {
                playerFound = true;
                Owner.LastKnowPlayerDistance = hit.distance;

                if (hit.distance > Owner.ShotRange )
                {
                    Owner.AddInput(Owner.GetLookDirection());
                }

                Owner.ApplyFlip(player.transform.position.x > Owner.transform.position.x);
                break;
            }
        }

        if (!playerFound)
        {
            Owner.LastKnowPlayerDistance = -1;
        }
    }


    public override void Exit()
    {
        base.Exit();
        Owner.LastKnowPlayerDistance = -1;  // Reseta ao sair do estado
        Owner.FieldOfVision = originalFildOfVision;
    }
}
