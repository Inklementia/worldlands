using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState StateData;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    public PlayerDetectedState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : 
        base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
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
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
    }
}
