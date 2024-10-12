using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{

    [SerializeField] private float maxPatrolDistance = 5f;

    [SerializeField] private float fieldOfVision = 10;
    [SerializeField] private float shotRange = 15;
    [SerializeField] private int rayNumberVision = 8;
    [SerializeField] private GameObject fireball;

    public float MaxPatrolDistance => maxPatrolDistance;
    public GameObject Fireball => fireball; 
    public int RayNumberVision => rayNumberVision;
    public float ShotRange => shotRange;
    public float FieldOfVision
    {
        get => fieldOfVision;
        set { fieldOfVision = value; }
    }


    public float LastKnowPlayerDistance { get; set; } = -1;
    
    public override void Start()
    {
        base.Start();
        StateMachine = new WizardStateMachine();
        StateMachine?.Initialize(new WizardPatrolState(), this);

        
    }



}
