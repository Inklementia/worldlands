using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_DeadState : DeadState
{
    private MeleeEnemy _enemy;

    public MeleeEnemy_DeadState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_DeadState stateData, MeleeEnemy enemy) : 
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
