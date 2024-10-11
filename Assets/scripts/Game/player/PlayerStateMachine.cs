using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public enum States
    {
        Idle, Run, Jump, Attack
    }

    private Dictionary<States, IState> states;

    public override void Initialize(IState startingState, Character owner)
    {
        base.Initialize(startingState, owner);

        states = new Dictionary<States, IState>
        {
            { States.Idle, new PlayerIdleState() },
            { States.Run, new PlayerRunState() },
            { States.Jump, new PlayerJumpState() },
            { States.Attack, new PlayerAttack1State() }
        };

        var Owner = (Player)owner;

        // Qualquer Estado -> Attack: Transição para o estado de ataque se o jogador iniciar um ataque
        TransitionManager.AddAnyStateTransition(
            states[States.Attack],
            () => Owner.InputProvider.IsAttacking
        );

        // Qualquer Estado -> Jump: Transição para o estado de pulo se o jogador apertar o botão de pulo, estiver no chão, e não estiver atacando
        TransitionManager.AddAnyStateTransition(
            states[States.Jump],
            () => Owner.InputProvider.IsJumping && Owner.IsGrounded && Owner.StateMachine.CurrentState != states[States.Attack]
        );

        // Attack -> Idle: Transição para o estado parado quando a animação de ataque termina
        TransitionManager.AddTransition(
            states[States.Attack], states[States.Idle],
            () => Owner.IsAnimationFinished()
        );

        // Attack -> Run: Transição para o estado de corrida quando a animação de ataque termina, o jogador está no chão e há movimento horizontal
        TransitionManager.AddTransition(
            states[States.Attack], states[States.Run],
            () => Owner.IsAnimationFinished() && Owner.IsGrounded && Mathf.Abs(Owner.InputProvider.HorizontalAxis) > 0.01f
        );

        // Jump -> Idle: Transição para o estado parado após o pulo, se a velocidade horizontal for insignificante e o jogador estiver no chão
        TransitionManager.AddTransition(
            states[States.Jump], states[States.Idle],
            () => Mathf.Abs(Owner.Body.velocity.x) < 0.01f && Owner.IsGrounded
        );

        // Jump -> Run: Transição para o estado de corrida após o pulo, se houver movimento horizontal e o jogador estiver no chão
        TransitionManager.AddTransition(
            states[States.Jump], states[States.Run],
            () => Mathf.Abs(Owner.Body.velocity.x) > 0.01f && Owner.IsGrounded
        );

        // Idle -> Run: Transição para o estado de corrida se houver movimento horizontal enquanto o jogador está parado
        TransitionManager.AddTransition(
            states[States.Idle], states[States.Run],
            () => Mathf.Abs(Owner.InputProvider.HorizontalAxis) > 0
        );

        // Run -> Idle: Transição para o estado parado se o jogador estiver no chão e a velocidade horizontal for insignificante
        TransitionManager.AddTransition(
            states[States.Run], states[States.Idle],
            () => Mathf.Abs(Owner.Body.velocity.x) < 0.01f && Owner.IsGrounded
        );
    }

    public void TransitionTo(States newState)
    {
        TransitionTo(states[newState]);
    }
}
