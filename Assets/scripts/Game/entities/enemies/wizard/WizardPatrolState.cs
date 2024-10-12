using UnityEngine;

public class WizardPatrolState : WizardState
{
    private float distanceTravelled = 0f;
    private int direction = 1; 
    private float originalFildOfVison;

    public override void Enter(Character owner)
    {
        base.Enter(owner);
        Owner.Animator.Play("WizardRun");
        originalFildOfVison = Owner.FieldOfVision;
        Owner.FieldOfVision *= 0.5f;
    }
    public override void Exit()
    {
        base.Exit();
        Owner.FieldOfVision = originalFildOfVison;
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Owner.AddInput(direction);

        distanceTravelled += Mathf.Abs(Owner.MaxSpeed * Time.fixedDeltaTime);

        if (distanceTravelled >= Owner.MaxPatrolDistance)
        {
            direction *= -1;
            distanceTravelled = 0f;
        }

        bool playerDetected = false;  // Verifica se o jogador foi detectado

        RaycastHit2D[] hits = Owner.RaycastHalfCircle(Owner.FieldOfVision, Owner.RayNumberVision);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                GameObject gameObject = hit.collider.gameObject;

                if (gameObject.GetComponent<Player>() != null)
                {
                    Owner.LastKnowPlayerDistance = hit.distance;
                    playerDetected = true;  // Jogador detectado
                    break;  // Sai do loop, pois o jogador foi encontrado
                }
            }
        }

        // Se o jogador não foi detectado, resetar a distância
        if (!playerDetected)
        {
            Owner.LastKnowPlayerDistance = -1;
        }

        // Inverte a direção do sprite
        Vector3 Scale = Owner.transform.localScale;
        Scale.x = Owner.Body.velocity.x > 0 ? 1 : -1;
        Owner.transform.localScale = Scale;
    }

}
