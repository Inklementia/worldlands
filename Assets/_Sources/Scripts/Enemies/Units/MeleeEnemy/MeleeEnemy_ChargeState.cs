using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_ChargeState : ChargeState
{
    private MeleeEnemy _enemy;

    public MeleeEnemy_ChargeState(Entity etity, FiniteStateMashine stateMachine, string animBoolName, D_ChargeState stateData, MeleeEnemy enemy) : 
        base(etity, stateMachine, animBoolName, stateData)
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
        else if (IsDetectingWall)
        {
            StateMachine.ChangeState(_enemy.IdleState);
        }
        else if (IsChargeTimeOver)
        {
            if (IsPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else
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
