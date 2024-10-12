using System.Collections;
using System.Collections.Generic;
using Game.Events;
using UnityEngine;

public class Fireball : Pawn
{

    public Vector2 Direction { private get;  set; }
    public CharacterStatus Status { private get; set; }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AddInput(Direction);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
     
        Character character = collision.gameObject.GetComponent<Character>();

        if (character != null)
        {
    
            var handle = EventManager.Instance.GetEventHandle<CharacterEventHandle>();
            handle.OnCharacterDamage.Invoke(character.ID, Status.Damage);

        }
    }
}
