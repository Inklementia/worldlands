using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    // state for moving (like patroling but with no area restriction it will move until meet a wall => just crossing room, back and forth)
    protected D_MoveState StateData;
    protected Vector2 Direction;

    protected float SpendTime;
    protected float TimeBeforeDetectingWall = .5f;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;
    protected bool IsDetectingWall;

    public MoveState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_MoveState stateData) : 
        base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.Core.CollisionSenses.Wall;
        IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
        IsPlayerInMaxAgroRange = Entity.Core.PlayerDetectionSenses.InMaxAgroRange;
        //Debug.Log("IsDetectingWall " + IsDetectingWall);
        //Debug.Log("IsPlayerInMinAgroRange " + IsPlayerInMinAgroRange);
        //Debug.Log("IsPlayerInMaxAgroRange " + IsPlayerInMaxAgroRange);
    }

    public override void Enter()
    {
        base.Enter();
 
        SpendTime = 0.0f;
        ChangeMoveDirection();
        HandleFlip();
        
    }

    public override void Exit()
    {
        base.Exit();
        SpendTime = 0.0f;
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
        Direction = new Vector2(-Direction.x + Random.Range(-1f, 1f), -Direction.y + Random.Range(-1f, 1f));
        Direction.Normalize();
        //Direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Debug.Log("Movement direction: " + Direction);
    }

    protected void HandleFlip()
    {
        if (Direction.x > 0)
        {
            Entity.Core.Movement.Flip180();
        }
        else if (Direction.x < 0)
        {
            Entity.Core.Movement.Flip0();
        }

    }
}
