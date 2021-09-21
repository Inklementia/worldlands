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

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.CheckWall();
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        // set velocity or transform
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

    protected void ChangeMoveDirection()
    {
        MovePos = new Vector2(
                 Random.Range(Entity.StartingPos.x - StateData.PatrolDistance - 1, Entity.StartingPos.x + StateData.PatrolDistance - 1),
                 Random.Range(Entity.StartingPos.y - StateData.PatrolDistance - 1, Entity.StartingPos.y + StateData.PatrolDistance - 1)
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
