using UnityEngine;

public class WizardAttack01State : WizardState
{
    private float originalFildOfVision;
    public override void Enter(Character onwer)
    {
        base.Enter(onwer);
        Owner.Animator.Play("WizardAttack01");
        originalFildOfVision = Owner.FieldOfVision;
        Owner.FieldOfVision = Owner.ShotRange;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        RaycastHit2D[] hits =  Owner.RaycastHalfCircle(Owner.FieldOfVision, Owner.RayNumberVision);

        if(hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Player player = hit.collider.gameObject.GetComponent<Player>();
                Owner.LastKnowPlayerDistance = hit.distance;
                Owner.ApplyFlip(player.transform.position.x > Owner.transform.position.x);
            }
        }
        else
        {
            Owner.LastKnowPlayerDistance = -1;
        }
        
    }
    public override void Exit()
    {
        base.Exit();
        Owner.FieldOfVision= originalFildOfVision;
        Owner.LastKnowPlayerDistance = -1;
    }
}