using System;
using Game.Events;
using Unity.VisualScripting;
using UnityEngine;


public class Player : Character
{
    [SerializeField] private float jumpForce = 8f;

    private PlayerInputProvider inputProvider;

    public float JumpForce=> jumpForce;
    public PlayerInputProvider InputProvider => inputProvider;

    public  override  void Awake()
    {
        base.Awake();
        inputProvider = new PlayerInputProvider();
        EventManager.Instance.AddEventHandle<PlayerEventHandle>();
    }
    public override  void Start()
    {
        base.Start();
        StateMachine = new PlayerStateMachine();
        StateMachine?.Initialize(new PlayerIdleState(), this);

    }

    public override void Update()
    {
        base.Update();
        InputProvider.Update();
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
 
        if (character != null)
        {
            var handle = EventManager.Instance.GetEventHandle<CharacterEventHandle>();
            handle.OnCharacterDamage.Invoke(character.ID ,Status.Damage);
        }
    }



}