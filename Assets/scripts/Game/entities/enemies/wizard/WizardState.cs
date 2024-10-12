using UnityEngine;

public class WizardState : IState
{
    private Character owner;

    public Wizard Owner { get => owner as Wizard; }
    public virtual void Enter(Character onwer)
    {
        this.owner = onwer;
    }

    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
    public virtual void Exit() { }
    public virtual void Update() { }

}
