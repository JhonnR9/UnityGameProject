using UnityEngine;


public class WizardAttack01State : WizardState
{
    private float originalFieldOfVision;
    private Transform fireSpawnPoint;
    Vector3 playerPosition = Vector3.zero;

    float time = 2;
    float cTime = 0;

    public override void Enter(Character owner)
    {
        base.Enter(owner);
        Owner.Animator.Play("WizardAttack");
        originalFieldOfVision = Owner.FieldOfVision;

        if (fireSpawnPoint == null)
        { 
            fireSpawnPoint = Owner.transform.GetChild(0); 

        }

    }

    public override void FixedUpdate()
    {
        cTime += Time.deltaTime;
        base.FixedUpdate();
        if (cTime >time)
        {
            
            var fire = GameObject.Instantiate(Owner.Fireball, fireSpawnPoint.position, Quaternion.identity);
            fire.GetComponent<SpriteRenderer>().sortingLayerName = ("entity");

            Fireball fireball = fire.GetComponent<Fireball>();
            fireball.Direction = ( playerPosition - Owner.transform.position );
            fireball.Status = Owner.Status;

            Owner.Animator.Play("WizardAttack");
            cTime = 0;
        }
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
                        playerPosition = player.transform.position;

                        // Ajusta a direção para olhar em direção ao jogador
                        Owner.ApplyFlip(player.transform.position.x > Owner.transform.position.x);
                        break;  // Sai do loop ao encontrar o jogador
                    }
                }
            }
        }

        // Se o jogador não foi encontrado, reseta a distância
        if (!playerFound)
        {
            Owner.LastKnowPlayerDistance = -1;
        }
    }

    public override void Exit()
    {
        base.Exit();

        Owner.FieldOfVision = originalFieldOfVision;  // Restaura o campo de visão original



    }
}
