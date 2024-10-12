using System.Collections;
using System.Collections.Generic;
using Game.Events;
using UnityEngine;

public class Fireball : Pawn
{

    public Vector2 Direction { private get;  set; }
    public CharacterStatus Status { private get; set; }
    private CharacterEventHandle handle;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AddInput(Direction);
    }

    public override void Awake()
    {
        base.Awake();
        handle = events.GetEventHandle<CharacterEventHandle>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();

        if (character != null)
        { 
            handle.OnCharacterDamage.Invoke(character.ID, Status.Damage);
            Destroy(this.gameObject);
            
        }
    }
}
