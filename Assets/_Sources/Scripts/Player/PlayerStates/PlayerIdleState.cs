using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : 
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
        Player.SetVelocity(0f, 0f);
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
