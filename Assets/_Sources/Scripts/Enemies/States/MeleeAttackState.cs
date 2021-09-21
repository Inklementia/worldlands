using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState StateData;

    protected AttackDetails AttackDetails;

    protected bool PerformCloseRangeAction;

    public MeleeAttackState(Entity etity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : 
        base(etity, stateMachine, animBoolName, attackPosition)
    {
       StateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        AttackDetails.DamageAmount = StateData.AttackDamage;
        AttackDetails.Position = Entity.AliveGO.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position, StateData.AttackRadius, StateData.WhatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", AttackDetails);
        }
    }
}
