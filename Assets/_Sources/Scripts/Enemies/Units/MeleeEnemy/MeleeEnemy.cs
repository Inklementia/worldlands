using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Entity
{
    public MeleeEnemy_IdleState IdleState { get; private set; }
    public MeleeEnemy_PatrolState PatrolState { get; private set; }
    public MeleeEnemy_PlayerDetectedState PlayerDetectedState { get; private set; }
    public MeleeEnemy_ChargeState ChargeState { get; private set; }
    public MeleeEnemy_MeleeAttackState MeleeAttackState { get; private set; }
    public MeleeEnemy_DamageState DamageState { get; private set; }
    public MeleeEnemy_DeadState DeadState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_DamageState damageStateData;
    [SerializeField] private D_DeadState deadStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        IdleState = new MeleeEnemy_IdleState(this, StateMachine, "idle", idleStateData, this);
        PatrolState = new MeleeEnemy_PatrolState(this, StateMachine, "move", patrolStateData, this);
        PlayerDetectedState = new MeleeEnemy_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
        ChargeState = new MeleeEnemy_ChargeState(this, StateMachine, "charge", chargeStateData, this);
        MeleeAttackState = new MeleeEnemy_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        DamageState = new MeleeEnemy_DamageState(this, StateMachine, "damage", damageStateData, this);
        DeadState = new MeleeEnemy_DeadState(this, StateMachine, "dead", deadStateData, this);

        StateMachine.Initialize(PatrolState);

    }


    public override void Update()
    {
        base.Update();
        //TestTarget.position = PatrolState.MovePos;

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(StartingPos, patrolStateData.PatrolDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.AttackRadius);
        //Gizmos.DrawLine(AliveGO.transform.position, new Vector2(AliveGO.transform.position.x + MoveState.Direction.x, AliveGO.transform.position.y + MoveState.Direction.y));
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        StateMachine.ChangeState(DamageState);

        if (_isDead)
        {
            StateMachine.ChangeState(DeadState);
        }
    }
}
