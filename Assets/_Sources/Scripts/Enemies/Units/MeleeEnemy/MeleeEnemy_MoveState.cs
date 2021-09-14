using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_MoveState : MoveState
{
    private MeleeEnemy _enemy;

    public MeleeEnemy_MoveState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_MoveState stateData, MeleeEnemy enemy) : 
        base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.SetVelocity(Direction, StateData.MovementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsDetectingWall)
        {
            SpendTime += Time.deltaTime;
            if (SpendTime >= TimeBeforeDetectingWall || Direction == Vector2.zero)
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
