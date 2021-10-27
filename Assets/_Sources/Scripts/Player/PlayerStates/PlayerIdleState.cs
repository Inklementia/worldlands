using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private string _animBoolName;
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : 
        base(player, stateMachine, playerData)
    {
        _animBoolName = animBoolName;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Player.Anim.SetBool(_animBoolName, true);
        Player.SetVelocity(0f, 0f);
    }

    public override void Exit()
    {
        base.Exit();
        Player.Anim.SetBool(_animBoolName, false);
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
