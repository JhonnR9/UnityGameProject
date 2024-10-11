using System.Collections.Generic;
using UnityEngine;

public class WizardStateMachine : StateMachine
{
    GameObject player;
    public enum States
    {
        Idle, Patrol, Chase, Attack1, Attack2, Scape
    }

    private Dictionary<States, IState> states;

    public override void Initialize(IState startingState, Character owner)
    {
        base.Initialize(startingState, owner);

        states = new Dictionary<States, IState>
        {
            { States.Patrol, new WizardPatrolState() },
            { States.Idle, new WizardIdleState() },
            { States.Chase, new WizardChaseState()},
            { States.Attack1, new WizardAttack01State()},
            { States.Attack2, new WizardAttack02State()},
            { States.Scape, new WizardEscapeState()}
        };

        Wizard Owner = owner as Wizard;

        // Patrol -> Chase: Se o jogador estiver dentro do campo de visão
        TransitionManager.AddTransition(states[States.Patrol], states[States.Chase],
            () => Owner.LastKnowPlayerDistance != -1 
        );

        // Chase -> Attack1: Se o jogador estiver dentro da distância de ataque
        TransitionManager.AddTransition(states[States.Chase], states[States.Attack1],
            () => Owner.LastKnowPlayerDistance < Owner.ShotRange  && Owner.LastKnowPlayerDistance > -1
        );

        // Attack1 -> Chase: Se o jogador se afastar após o ataque, mas ainda estiver visível
        TransitionManager.AddTransition(states[States.Attack1], states[States.Chase],
            () => Owner.LastKnowPlayerDistance > Owner.ShotRange && Owner.LastKnowPlayerDistance < Owner.FieldOfVision 
        );

        // Chase -> Patrol: Se o mago perder completamente a visão do jogador
        TransitionManager.AddTransition(states[States.Chase], states[States.Patrol],
            () => Owner.LastKnowPlayerDistance == -1 
        );

    }

    public void TransitionTo(States newState)
    {
        TransitionTo(states[newState]);
    }
}
