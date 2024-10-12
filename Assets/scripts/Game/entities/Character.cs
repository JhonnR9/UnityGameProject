using System;
using System.Collections;
using System.Collections.Generic;
using Game.Events;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterStatus))]
public class Character : Pawn 
{
    private Animator animator;
    private CharacterStatus status;
    public Animator Animator => animator;
    public StateMachine StateMachine { get; protected set; }
    public CharacterStatus Status => status;
    public string ID { get; private set; }


    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
        EventManager.Instance.AddEventHandle<CharacterEventHandle>();
        ID = Guid.NewGuid().ToString();
    }

    public virtual void Start()
    {
        var handle = EventManager.Instance.GetEventHandle<CharacterEventHandle>();
        handle.OnCharacterDamage += OnDamage;
    }
    public virtual void OnDamage(string id ,float damageAmount)
    {
    
        if (id == ID) 
        {
            status.Life -= damageAmount;
        }
    }



    public bool IsAnimationFinished()
    {
        AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !Animator.IsInTransition(0);

    }

    public RaycastHit2D[] MakeRaycast(Vector2 direction, float distance, int bufferSize)
    {
        RaycastHit2D[] results = new RaycastHit2D[bufferSize];

        Body.Cast(new Vector2(GetLookDirection(), 0), results, distance);

        return results;
    }

    public int GetLookDirection()
    {
        return transform.localScale.x > 0 ? 1 : -1;
    }

    public RaycastHit2D[] RaycastHalfCircle(float distance, int numberOfRays)
    {
        List<RaycastHit2D> hitResults = new List<RaycastHit2D>();
        float angleStep = 180f / (numberOfRays - 1);
        float startAngle = 0f; 

        for (int i = 0; i < numberOfRays; i++)
        {
            // Calcula o ângulo atual em radianos e gera a direção do Raycast
            float currentAngle = startAngle + i * angleStep;
            float radianAngle = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

            RaycastHit2D[] hits = new RaycastHit2D[1];
            // Dispara o Raycast
            int hitCount = Body.Cast( direction, hits, distance);


            // Adiciona o resultado à lista se houver colisão
            if (hits[0].collider != null)
            {
                hitResults.Add(hits[0]);
            }

            // Debug para ver os raios no editor
            Debug.DrawRay(transform.position, direction * distance, Color.red);
        }

        return hitResults.ToArray();
    }

    public virtual void Update()
    {
        StateMachine?.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachine?.FixedUpdate();
    }

    public virtual void LateUpdate()
    {
        StateMachine?.LateUpdate();
    }

}
