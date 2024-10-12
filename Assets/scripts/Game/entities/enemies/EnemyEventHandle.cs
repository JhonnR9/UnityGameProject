
using System;
using UnityEngine;

public class EnemyEventHandle : EventHandle
{
    public Action<Vector3> OnPlayerPositionChanged;
    public Action<CharacterStatus> OnPlayerDamage;
}