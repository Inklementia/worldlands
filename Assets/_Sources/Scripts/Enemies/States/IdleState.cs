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
    protected bool IsPlayerInMaxAgroRange;

    public IdleState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_IdleState stateData) : 
        base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        IsIdleTimeOver = false;

        IsDetectingWall = Entity.CheckWall();
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
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
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
    }

    public void SetRandomIdleTime()
    {
        IdleTime = Random.Range(StateData.MinIdleTime, StateData.MaxIdleTime);
    } 
}
