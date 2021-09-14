using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_PatrolState : PatrolState
{
    private MeleeEnemy _enemy;
    public MeleeEnemy_PatrolState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PatrolState stateData, MeleeEnemy enemy) : 
        base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        ChangeMoveDirection();
        HandleFlip();
        _enemy.GoTo(MovePos, StateData.MovementSpeed);
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMinAgroRange)
        {
            StateMachine.ChangeState(_enemy.PlayerDetectedState);
        }
        else if (IsDetectingWall || Vector2.Distance(_enemy.AliveGO.transform.position, MovePos) < 0.1f)
        {
            StateMachine.ChangeState(_enemy.IdleState);
        }
     
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

 
}
