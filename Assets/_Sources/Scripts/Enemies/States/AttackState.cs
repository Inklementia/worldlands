using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform AttackPosition;

    protected bool IsAnimationFinished;
    protected bool IsPlayerInMinAgroRange;

    public AttackState(Entity etity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition) : 
        base(etity, stateMachine, animBoolName)
    {
        AttackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerInMinAgroRange = Entity.Core.CollisionSenses.CheckIfPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        Entity.AnimationToStateMachine.AttackState = this;
        IsAnimationFinished = false;
        Entity.StopMovement();
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

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        IsAnimationFinished = true;
    }
}

