using UnityEngine;

public class WizardAttack01State : WizardState
{
    private float originalFieldOfVision;

    public override void Enter(Character owner)
    {
        base.Enter(owner);
        Owner.Animator.Play("WizardAttack01");
        originalFieldOfVision = Owner.FieldOfVision;
       
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        RaycastHit2D[] hits = Owner.RaycastHalfCircle(Owner.FieldOfVision, Owner.RayNumberVision);

        bool playerFound = false;

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    Player player = hit.collider.gameObject.GetComponent<Player>();

                    if (player != null)
                    {
                        playerFound = true;  // Jogador encontrado
                        Owner.LastKnowPlayerDistance = hit.distance;

                        // Ajusta a dire��o para olhar em dire��o ao jogador
                        Owner.ApplyFlip(player.transform.position.x > Owner.transform.position.x);
                        break;  // Sai do loop ao encontrar o jogador
                    }
                }
            }
        }

        // Se o jogador n�o foi encontrado, reseta a dist�ncia
        if (!playerFound)
        {
            Owner.LastKnowPlayerDistance = -1;
        }
    }

    public override void Exit()
    {
        base.Exit();
        Owner.FieldOfVision = originalFieldOfVision;  // Restaura o campo de vis�o original
       
    }
}
