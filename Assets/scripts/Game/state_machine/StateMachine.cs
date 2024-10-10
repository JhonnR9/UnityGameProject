using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private HashSet<IState> states = new HashSet<IState>();

    public IState CurrentState { get; private set; }
    private Character owner;
    private StateTransitionManager transitionManager;

    public StateTransitionManager TransitionManager => transitionManager;

    public virtual void Initialize(IState startingState, Character owner)
    {
        transitionManager = new StateTransitionManager();
        AddState(startingState);
        CurrentState = startingState;
        CurrentState.Enter(owner);
        this.owner = owner;
        
    }

    public virtual void AddState(IState newState)
    {
        states.Add(newState);
    }

    public virtual void RemoveState(IState newState)
    {
        if (states.Contains(newState))
        {
           states.Remove(newState);
        }
 
    }

    public virtual void TransitionTo(IState nextState)
    {
        //Debug.Log(nextState);
        if (CurrentState == nextState) return;
        if (states.Contains(nextState))
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState.Enter(owner);
        }
        else
        {
            AddState(nextState);
            TransitionTo(nextState);
        }
    }

    public virtual void Update()
    {
        CurrentState?.Update();
        TransitionTo(transitionManager.GetNextState(CurrentState));
    }

    public virtual void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }

    public virtual void LateUpdate()
    {
        CurrentState?.LateUpdate();
    }
}
