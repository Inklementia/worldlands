using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;
using UnityEngine;

public class MeleeEnemy_PlayerDetectedState : PlayerDetectedState
{
    private MeleeEnemy _enemy;

    public MeleeEnemy_PlayerDetectedState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PlayerDetectedState stateData, MeleeEnemy enemy) : 
        base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.Core.Movement.StopMovement();
        //_enemy.ChargeState.SetWhereToCharge(PlayerDetectedAt);
        //Debug.Log("Player detected a: " + PlayerDetectedAt);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_enemy.MeleeAttackState);
        }
        else if (PerformLongRangeAction && Time.time >= StartTime + StateData.TimeBeforLongRangeAction)
        {
            StateMachine.ChangeState(_enemy.ChargeState);
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            //StateMachine.ChangeState(_enemy.PatrolState);
            StateMachine.ChangeState(_enemy.MoveState);

        }
        //else if (IsDetectingWall)
        //{
        //    // in idea:  flip and move, but...
        //    //StateMachine.ChangeState(_enemy.PatrolState);
        //    StateMachine.ChangeState(_enemy.MoveState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
