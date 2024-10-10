using UnityEngine;
using UnityEngine.UIElements;

public class PlayerRunState : PlayerState
{
    public override void Enter(Character onwer)
    {
        base.Enter(onwer);

        Owner.Animator.Play("SwordmanRun");
    }
    public override void FixedUpdate()
    {
        Owner.AddInput(Owner.InputProvider.HorizontalAxis);

        if (Owner.InputProvider.HorizontalAxis != 0)
        {
            Vector3 newScale = Owner.transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * (Owner.InputProvider.HorizontalAxis < 0 ? -1 : 1);
            Owner.transform.localScale = newScale;
        }

    }

}