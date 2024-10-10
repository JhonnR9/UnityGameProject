using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateTransition
{
    public IState FromState { get; }
    public IState ToState { get; }
    public Func<bool> Condition { get; }

    public StateTransition(IState fromState, IState toState, Func<bool> condition)
    {
        FromState = fromState;
        ToState = toState;
        Condition = condition;
    }
}

public class StateTransitionManager
{
    private Dictionary<String, List<StateTransition>> transitionRules = new Dictionary<String, List<StateTransition>>();
    private Dictionary< Func<bool>, IState> anyStateTransitionRules = new Dictionary<Func<bool>, IState>();
    public void AddTransition(IState fromState, IState toState, Func<bool> condition)
    {
        if (!transitionRules.ContainsKey(fromState.ToString()))
        {
            transitionRules[fromState.ToString()] = new List<StateTransition>();
        }

        transitionRules[fromState.ToString()].Add(new StateTransition(fromState, toState, condition));
    }

    public void AddAnyStateTransition(IState ToState, Func<bool> condition)
    {
        anyStateTransitionRules.Add(condition, ToState);
    }

    public IState GetNextState(IState currentState)
    {
        foreach (Func<bool> condition in anyStateTransitionRules.Keys)
        {
            if (condition.Invoke())
            {
                return anyStateTransitionRules[condition];
            }
        }
        if (transitionRules.ContainsKey(currentState.ToString()))
        {
            foreach (var transition in transitionRules[currentState.ToString()])
            {
                if (transition.Condition.Invoke())  // Verifica se a condição da transição é verdadeira
                {
                    return transition.ToState;  // Retorna o estado alvo da transição
                   
                }
            }
        }
        return currentState;  // Se nenhuma condição for satisfeita, permanece no estado atual
    }
}
