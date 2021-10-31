using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class Entity : MonoBehaviour
{
    public FiniteStateMashine StateMachine;
    public D_Entity EntityData;
    public Core Core { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }
    public GameObject Player { get; private set; }
    public Vector2 StartingPos { get; private set; }

    [SerializeField] private Tag playerTag;
    [SerializeField] private HealthSystem healthSystem;


    //public Transform TestTarget;

    private float _knockbackStartTime;

    private int _lastDamageDirection;

    protected bool IsDead;

    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();
        Core = GetComponentInChildren<Core>();

        Core.Movement.SetFacingDirection(-1);
        StartingPos = transform.position;

        StateMachine = new FiniteStateMashine();

        Player = gameObject.FindWithTag(playerTag);
    }

    public virtual void Update()
    {
        CheckKnockback();

        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void CheckKnockback()
    {
        if (Time.time >= _knockbackStartTime + EntityData.KnockbackDuration)
        {
            Core.Movement.SetVelocityZero();
        }
    }

    public virtual Vector2 GetPlayerPosition()
    {
        return Player.transform.position;
    }

    public virtual void GoTo(Vector2 point, float speed)
    {
        var distance = Vector2.Distance(transform.position, point);
        Rb.DOMove(point, distance / speed);
        //transform.position = Vector2.MoveTowards(transform.position, MovePos, speed * Time.deltaTime);
    }

    public virtual void StopMovement()
    {
        Rb.DOPause();
    }

    // DAMAGE 
    public virtual void Damage(AttackDetails attackDetails)
    {
        healthSystem.DecreaseHealth(attackDetails.DamageAmount);

        if(attackDetails.Position.x > transform.position.x)
        {
            _lastDamageDirection = -1;
        }
        else
        {
            _lastDamageDirection = 1;
        }

        _knockbackStartTime = Time.time;

        Core.Movement.SetVelocity(EntityData.KnockBackAngle, EntityData.KnockBackSpeed, _lastDamageDirection);

        if(healthSystem.GetCurrentHealth() <= 0)
        {
            IsDead = true;
        }
    }

    public virtual void OnDrawGizmos()
    {
    }
}
