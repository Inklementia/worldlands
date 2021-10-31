﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState StateData;

    protected bool IsDetectingWall;
    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    protected bool PerformLongRangeAction;
    protected bool PerformCloseRangeAction;

    //protected Vector2 PlayerDetectedAt;
    public PlayerDetectedState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : 
        base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        IsPlayerInMinAgroRange = Entity.Core.CollisionSenses.CheckIfPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange = Entity.Core.CollisionSenses.CheckIfPlayerInMaxAgroRange();
        PerformCloseRangeAction = Entity.Core.CollisionSenses.CheckIfPlayerInCloseRangeAction();
        IsDetectingWall = Entity.Core.CollisionSenses.CheckWall();

    }

    public override void Enter()
    {
        base.Enter();

        PerformLongRangeAction = false;
        //PlayerDetectedAt = Entity.GetPlayerPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= StartTime + StateData.TimeBeforLongRangeAction)
        {
            PerformLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
