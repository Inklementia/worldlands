using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Entity
{
    public MeleeEnemy_IdleState IdleState { get; private set; }
    //public MeleeEnemy_MoveState MoveState { get; private set; }
    public MeleeEnemy_PatrolState PatrolState { get; private set; }
    public MeleeEnemy_PlayerDetectedState PlayerDetectedState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    public override void Start()
    {
        base.Start();

        IdleState = new MeleeEnemy_IdleState(this, StateMachine, "idle", idleStateData, this);
        //MoveState = new MeleeEnemy_MoveState(this, StateMachine, "move", patrolStateData, this);
        PatrolState = new MeleeEnemy_PatrolState(this, StateMachine, "move", patrolStateData, this);
        PlayerDetectedState = new MeleeEnemy_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
        StateMachine.Initialize(PatrolState);
        //StateMachine.Initialize(MoveState);
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
        //Gizmos.DrawLine(AliveGO.transform.position, new Vector2(AliveGO.transform.position.x + MoveState.Direction.x, AliveGO.transform.position.y + MoveState.Direction.y));
    }
}
