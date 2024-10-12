
using System;
using UnityEngine;

public class PlayerEventHandle : EventHandle
{
    public Action<CharacterStatus> OnPlayerDamage { get; set; }
    // param string character ID
    public Action<string, CharacterStatus> OnPlayerGenerateID { get; set; }

}