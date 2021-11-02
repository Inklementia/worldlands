using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState StateData;

    protected float IdleTime;
    protected bool IsIdleTimeOver;

    protected bool IsDetectingWall;
    protected bool IsPlayerInMinAgroRange;

    public IdleState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_IdleState stateData) : 
        base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.Core.CollisionSenses.Wall;
        IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
    }

    public override void Enter()
    {
        base.Enter();
        IsIdleTimeOver = false;

        SetRandomIdleTime();
       
    }

    public override void Exit()
    {
        base.Exit();

        //flip after idle
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= StartTime + IdleTime)
        {
            IsIdleTimeOver = true;
        }

      
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetRandomIdleTime()
    {
        IdleTime = Random.Range(StateData.MinIdleTime, StateData.MaxIdleTime);
    } 
}
