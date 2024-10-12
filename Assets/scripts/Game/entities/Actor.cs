using UnityEngine;
using Game.Events;

public class Actor : MonoBehaviour
{
    protected static EventManager events;

    public virtual void Awake()
    {
        if (events == null) // Garante que EventManager s� ser� instanciado uma vez
        {
            events = new EventManager();
        }

        RegisterEvents(events);

    }

    public virtual void Start()
    {
        BindEvents(events);
    }

    public virtual void RegisterEvents(EventManager e)
    {
        // Registra eventos espec�ficos deste ator
    }

    public virtual void BindEvents(EventManager e)
    {
        // Vincula eventos espec�ficos deste ator
    }
}
