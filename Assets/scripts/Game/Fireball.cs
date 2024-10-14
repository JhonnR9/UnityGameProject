
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Fireball : Pawn
{

    public Vector2 Direction { private get;  set; }
    public CharacterStatus Status { private get; set; }
    private CharacterEventHandle handle;
    Animator animator;
    private bool isColliding =false;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isColliding)
        {
           // Debug.Log($"x: {(Direction).normalized.x}, y: {(Direction).normalized.y}");
           AddInput(Direction.normalized);
            //Body.velocity = Direction * 10;
        }
    }

    public override void Awake()
    {
        base.Awake();
        handle = events.GetEventHandle<CharacterEventHandle>();
        animator = GetComponent<Animator>();
    }

    public  void Update()
    {
        if (isColliding && IsAnimationFinished(animator)) 
        {
            Destroy(this.gameObject);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        animator.Rebind();
        animator.Play("FireballExplosion");
        isColliding = true;

        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        { 
            handle.OnCharacterDamage.Invoke(character.ID, Status.Damage);
        }
    }
}
