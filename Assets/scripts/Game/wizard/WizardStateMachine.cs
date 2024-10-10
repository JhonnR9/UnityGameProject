using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
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
            () => Owner.LastKnowPlayerDistance != -1 && Owner.LastKnowPlayerDistance < Owner.FieldOfVision
        );

        // Chase -> Attack1: Se o jogador estiver dentro da distância de ataque
        TransitionManager.AddTransition(states[States.Chase], states[States.Attack1],
            () => Owner.LastKnowPlayerDistance <= Owner.ShotRange 
        );

        // Attack1 -> Chase: Se o jogador se distanciar após o ataque ou perder o rastro
        TransitionManager.AddTransition(states[States.Attack1], states[States.Chase],
            () =>  Owner.LastKnowPlayerDistance == -1 
        );

        /*/ Chase -> Patrol: Se o mago perder completamente a visão do jogador
        TransitionManager.AddTransition(states[States.Chase], states[States.Patrol],
            () => Owner.LastKnowPlayerDistance == -1
        );*/



    }

    public void TransitionTo(States newState)
    {
        TransitionTo(states[newState]);
    }


}
