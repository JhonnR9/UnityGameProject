using UnityEngine;


public class PlayerState : IState
{
    protected PlayerStateMachine stateMachine;
    private Character owner;

    public Player Owner { get => owner as Player; }
    public virtual void Enter(Character onwer)
    {
        this.owner = onwer;
        stateMachine = onwer.StateMachine as PlayerStateMachine;
        
    }

    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
    public virtual void Exit(){}
    public virtual void Update(){}

}
