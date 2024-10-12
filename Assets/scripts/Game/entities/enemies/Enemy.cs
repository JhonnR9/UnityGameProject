using Game.Events;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Enemy : Character
{

    public override void Awake()
    {
        base.Awake();
        EventManager.Instance.AddEventHandle<EnemyEventHandle>();
    }



}
