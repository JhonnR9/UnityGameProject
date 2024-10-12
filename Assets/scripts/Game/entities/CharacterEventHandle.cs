
using System;
using UnityEngine;

public class CharacterEventHandle : EventHandle
{
    // param int is amount of damage
    public Action<string,float> OnCharacterDamage;
}