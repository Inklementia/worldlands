using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    // state for patroling certain area
    protected D_PatrolState StateData;

    protected Vector2 MovePos;
    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;
    protected bool IsDetectingWall;

    public PatrolState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PatrolState stateData) : 
        base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        IsDetectingWall = Entity.CheckWall();
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange  = Entity.CheckPlayerInMaxAgroRange();
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
        IsDetectingWall = Entity.CheckWall();
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
    }

    protected void ChangeMoveDirection()
    {
        MovePos = new Vector2(
                 Random.Range(Entity.StartingPos.x - StateData.PatrolDistance, Entity.StartingPos.x + StateData.PatrolDistance),
                 Random.Range(Entity.StartingPos.y - StateData.PatrolDistance, Entity.StartingPos.y + StateData.PatrolDistance)
                 );
    }

    protected void HandleFlip()
    {
        if (MovePos.x > Entity.AliveGO.transform.position.x)
        {
            Entity.Flip180();
        }
        else if(MovePos.x < Entity.AliveGO.transform.position.x)
        {
            Entity.Flip0();
        }
    }
}
