using System;
using Game.Events;
using Unity.VisualScripting;
using UnityEngine;


public class Player : Character
{
    [SerializeField] private float jumpForce = 8f;

    private PlayerInputProvider inputProvider;
    private PlayerEventHandle handle;

    public float JumpForce=> jumpForce;
    public PlayerInputProvider InputProvider => inputProvider;

    public  override  void Awake()
    {
        base.Awake();
        inputProvider = new PlayerInputProvider();
        EventManager.Instance.AddEventHandle<PlayerEventHandle>();
    }
    public  void Start()
    {
        StateMachine = new PlayerStateMachine();
        StateMachine?.Initialize(new PlayerIdleState(), this);
      
        handle = EventManager.Instance.GetEventHandle<PlayerEventHandle>();
    }

    public override void Update()
    {
        base.Update();
        InputProvider.Update();
        Debug.Log(StateMachine.CurrentState);
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        handle.OnPlayerPositionChanged?.Invoke(transform.position);
    }



}