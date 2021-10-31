using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerCanCombatState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) :
        base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Core.Movement.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
      
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Input.x != 0f && Input.y != 0f)
        {
            StateMachine.ChangeState(Player.MoveState);
        }
     
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
