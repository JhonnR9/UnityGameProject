using UnityEngine;
using Game.Events;

public class WizardChaseState : WizardState
{
    private float originalFieldOfVision;

    public override void Enter(Character owner)
    {
        base.Enter(owner);
        Owner.Animator.Play("WizardRun");
        originalFieldOfVision = Owner.FieldOfVision;
        Owner.FieldOfVision = Owner.FieldOfVision * 1.5f;  // Aumentar levemente o campo de visão para maior detecção durante a perseguição
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        RaycastHit2D[] hits = Owner.RaycastHalfCircle(Owner.FieldOfVision, Owner.RayNumberVision);
        bool playerFound = false;

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                GameObject gameObject = hit.collider.gameObject;
                Player player = gameObject.GetComponent<Player>();

                if (player != null)
                {
                    playerFound = true;
                    Owner.LastKnowPlayerDistance = hit.distance;

                    // Se o jogador está fora do alcance de tiro, continua perseguindo
                    if (hit.distance > Owner.ShotRange)
                    {
                        Owner.AddInput(Owner.GetLookDirection());
                    }

                    // Aplica flip para olhar na direção correta
                    Owner.ApplyFlip(player.transform.position.x > Owner.transform.position.x);
                    break;  // Encerra o loop quando o jogador for encontrado
                }
            }
        }

        // Se o jogador não for encontrado, reseta a distância para indicar perda de visão
        if (!playerFound)
        {
            Owner.LastKnowPlayerDistance = -1;
        }
    }

    public override void Exit()
    {
        base.Exit();
        Owner.LastKnowPlayerDistance = -1;  // Reseta ao sair do estado
        Owner.FieldOfVision = originalFieldOfVision;  // Restaura o campo de visão original
    }
}
