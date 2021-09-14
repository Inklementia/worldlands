using System.Collections;
using System.Collections.Generic;
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
        _enemy.StopMovement();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!IsPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(_enemy.IdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
