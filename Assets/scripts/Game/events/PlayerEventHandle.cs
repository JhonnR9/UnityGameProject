
using System;
using UnityEngine;

public class PlayerEventHandle : EventHandle
{
    public Action<Vector3> OnPlayerPositionChanged;
    
}