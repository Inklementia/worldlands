using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

public class MeleeEnemy_ChargeState : ChargeState
{
    private MeleeEnemy _enemy;

    public MeleeEnemy_ChargeState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_ChargeState stateData, MeleeEnemy enemy) : 
        base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
        else if (IsChargeTimeOver)
        {

            if (IsPlayerInMaxAgroRange)
            {
                StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(_enemy.IdleState);
            }
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            if (IsDetectingWall)
            {
                StateMachine.ChangeState(_enemy.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
