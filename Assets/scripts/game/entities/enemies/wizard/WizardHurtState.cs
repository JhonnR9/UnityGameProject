using UnityEngine;

public class WizardHurtState : WizardState
{
    private float knockbackForce = 5f; 


    public override void Enter(Character owner)
    {
        base.Enter(owner);
        Knockback();
    }



    void Knockback()
    {
        Vector2 knockbackDirection = -Owner.Body.velocity.normalized;
        Vector2 knockbackForceVector = knockbackDirection * knockbackForce;

        Owner.Body.AddForce(knockbackForceVector, ForceMode2D.Impulse);
    }
}
