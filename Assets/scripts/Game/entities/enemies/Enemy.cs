using Game.Events;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Enemy : Character
{

    public override void Awake()
    {
        base.Awake();
       
    }

    public override void RegisterEvents(EventManager e)
    {
        base.RegisterEvents(e);
        e.AddEventHandle<EnemyEventHandle>();
    }



}
