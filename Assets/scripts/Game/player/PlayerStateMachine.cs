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

        // Qualquer Estado -> Attack: Transi��o para o estado de ataque se o jogador iniciar um ataque
        TransitionManager.AddAnyStateTransition(
            states[States.Attack],
            () => Owner.InputProvider.IsAttacking
        );

        // Qualquer Estado -> Jump: Transi��o para o estado de pulo se o jogador apertar o bot�o de pulo, estiver no ch�o, e n�o estiver atacando
        TransitionManager.AddAnyStateTransition(
            states[States.Jump],
            () => Owner.InputProvider.IsJumping && Owner.IsGrounded && Owner.StateMachine.CurrentState != states[States.Attack]
        );

        // Attack -> Idle: Transi��o para o estado parado quando a anima��o de ataque termina
        TransitionManager.AddTransition(
            states[States.Attack], states[States.Idle],
            () => Owner.IsAnimationFinished()
        );

        // Attack -> Run: Transi��o para o estado de corrida quando a anima��o de ataque termina, o jogador est� no ch�o e h� movimento horizontal
        TransitionManager.AddTransition(
            states[States.Attack], states[States.Run],
            () => Owner.IsAnimationFinished() && Owner.IsGrounded && Mathf.Abs(Owner.InputProvider.HorizontalAxis) > 0.01f
        );

        // Jump -> Idle: Transi��o para o estado parado ap�s o pulo, se a velocidade horizontal for insignificante e o jogador estiver no ch�o
        TransitionManager.AddTransition(
            states[States.Jump], states[States.Idle],
            () => Mathf.Abs(Owner.Body.velocity.x) < 0.01f && Owner.IsGrounded
        );

        // Jump -> Run: Transi��o para o estado de corrida ap�s o pulo, se houver movimento horizontal e o jogador estiver no ch�o
        TransitionManager.AddTransition(
            states[States.Jump], states[States.Run],
            () => Mathf.Abs(Owner.Body.velocity.x) > 0.01f && Owner.IsGrounded
        );

        // Idle -> Run: Transi��o para o estado de corrida se houver movimento horizontal enquanto o jogador est� parado
        TransitionManager.AddTransition(
            states[States.Idle], states[States.Run],
            () => Mathf.Abs(Owner.InputProvider.HorizontalAxis) > 0
        );

        // Run -> Idle: Transi��o para o estado parado se o jogador estiver no ch�o e a velocidade horizontal for insignificante
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
