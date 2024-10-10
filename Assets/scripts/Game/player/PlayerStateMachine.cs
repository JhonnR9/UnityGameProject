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

        AddTransitions(owner);
    }

    private void AddTransitions(Character owner)
    {
        Player player = owner as Player;

        // Transições para ataque
        AddAttackTransitions(player);

        // Transições para pular
        AddJumpTransitions(player);

        // Transições para correr
        AddRunTransitions(player);

        // Transições para ficar parado
        AddIdleTransitions(player);
    }

    private void AddAttackTransitions(Player player)
    {
        TransitionManager.AddAnyStateTransition(
            states[States.Attack], () => player.InputProvider.IsAttacking
        );

        // Permitir voltar para Idle ou Run após o ataque, dependendo do estado anterior
        TransitionManager.AddTransition(
            states[States.Attack], states[States.Idle],
            () => player.IsAnimationFinished() && player.IsGrounded
        );
        TransitionManager.AddTransition(
            states[States.Attack], states[States.Run],
            () => player.IsAnimationFinished() && player.IsGrounded && Mathf.Abs(player.Body.velocity.x) > 0.01f
        );
    }

    private void AddJumpTransitions(Player player)
    {
        // Transição para Jump
        TransitionManager.AddTransition(
            states[States.Idle], states[States.Jump],
            () => player.InputProvider.IsJumping && player.IsGrounded
        );
        TransitionManager.AddTransition(
            states[States.Run], states[States.Jump],
            () => player.IsGrounded && player.InputProvider.IsJumping
        );


        // Retornar ao Idle ou Run após o salto
        TransitionManager.AddTransition(
            states[States.Jump], states[States.Idle],
            () => Mathf.Abs(player.Body.velocity.x) < 0.01f && player.IsGrounded
        );
        TransitionManager.AddTransition(
            states[States.Jump], states[States.Run],
            () => Mathf.Abs(player.Body.velocity.x) > 0.01f && player.IsGrounded
        );
    }

    private void AddIdleTransitions(Player player)
    {
        // Transição de Idle para Run
        TransitionManager.AddTransition(
            states[States.Idle], states[States.Run],
            () => Mathf.Abs(player.InputProvider.HorizontalAxis) > 0
        );

        // Retornar ao Idle se não houver movimento
        TransitionManager.AddTransition(
            states[States.Run], states[States.Idle],
            () => Mathf.Abs(player.Body.velocity.x) < 0.01f && player.IsGrounded
        );
    }

    private void AddRunTransitions(Player player)
    {
        // Transição de Idle para Run
        TransitionManager.AddTransition(
            states[States.Idle], states[States.Run],
            () => Mathf.Abs(player.InputProvider.HorizontalAxis) > 0 && player.IsGrounded // Correndo só se estiver no chão
        );

        // Impedir correr durante o salto
        TransitionManager.AddTransition(
            states[States.Jump], states[States.Run],
            () => false // Sempre falso, impedindo a transição
        );

    }


    public void TransitionTo(States newState)
    {
        TransitionTo(states[newState]);
    }
}
