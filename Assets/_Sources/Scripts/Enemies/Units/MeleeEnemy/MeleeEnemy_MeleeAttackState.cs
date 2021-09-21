using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_MeleeAttackState : MeleeAttackState
{
    private MeleeEnemy _enemy;

    public MeleeEnemy_MeleeAttackState(Entity etity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, MeleeEnemy enemy) : 
        base(etity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (!PerformCloseRangeAction)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
