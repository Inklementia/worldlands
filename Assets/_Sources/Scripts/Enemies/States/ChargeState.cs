using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState StateData;

    //protected Vector2 WhereToCharge;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsDetectingWall;
    protected bool PerformCloseRangeAction;

    protected bool IsChargeTimeOver;

    public ChargeState(Entity etity, FiniteStateMashine stateMachine, string animBoolName, D_ChargeState stateData) : base(etity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
        PerformCloseRangeAction = Entity.Core.PlayerDetectionSenses.InCloseRangeAction;
        IsDetectingWall = Entity.Core.CollisionSenses.Wall;
    }

    public override void Enter()
    {
        base.Enter();

        IsChargeTimeOver = false;
        HandleFlip();
        Entity.GoTo(Entity.GetTargetPosition(), StateData.ChargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.ChargeTime)
        {
            IsChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void HandleFlip()
    {
        if (Entity.Target.transform.position.x > Entity.transform.position.x)
        {
            Entity.Core.Movement.Flip180();
        }
        else if (Entity.Target.transform.position.x < Entity.transform.position.x)
        {
            Entity.Core.Movement.Flip0();
        }
    }

    //public void SetWhereToCharge(Vector2 target)
    //{
    //    WhereToCharge = target;
    //}

    
}