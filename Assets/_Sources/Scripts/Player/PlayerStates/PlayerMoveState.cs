using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private string _animBoolName;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) :
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
    }

    public override void Exit()
    {
        base.Exit();
        Player.Anim.SetBool(_animBoolName, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocity(Input.x * PlayerData.MovementVelocity, Input.y * PlayerData.MovementVelocity);
        CheckMovementDirection();

        if (Input.x == 0f && Input.y == 0f)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckMovementDirection()
    {
        if (Player.FacingDirection == 1 && Input.x < 0)
        {
            Player.Flip();
        }
        else if (Player.FacingDirection == -1 && Input.x > 0)
        {
            Player.Flip();
        }
    }

}
