
using Game.Events;
using UnityEngine;
using System.Threading.Tasks;


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
    
    }

    public override void RegisterEvents(EventManager e)
    {
        base.RegisterEvents(e);
        e.AddEventHandle<PlayerEventHandle>();

    }


    public override async void Start()
    {
        base.Start();

        StateMachine = new PlayerStateMachine();
        StateMachine?.Initialize(new PlayerIdleState(), this);

        await WaitForPlayerGenerateID();

        events.GetEventHandle<PlayerEventHandle>().OnPlayerGenerateID?.Invoke(ID, Status);
    }


    public override void OnDamage(string id, float damageAmount)
    {
        base.OnDamage(id, damageAmount);
    
    }

    public override void Update()
    {
        base.Update();
        InputProvider.Update();
   
    }

    private async Task WaitForPlayerGenerateID()
    {
        PlayerEventHandle playerEventHandle = events.GetEventHandle<PlayerEventHandle>();

        // Loop assíncrono para aguardar até que OnPlayerGenerateID não seja nulo
        while (playerEventHandle.OnPlayerGenerateID == null)
        {
            await Task.Yield();  // Espera um frame antes de checar novamente
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy character = collision.gameObject.GetComponent<Enemy>();
 
        if (character != null)
        {
            var handle = events.GetEventHandle<CharacterEventHandle>();
            handle.OnCharacterDamage.Invoke(character.ID ,Status.Damage);
        }
    }



}