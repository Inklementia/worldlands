using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerCanCombatState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) :
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
     
    }

    public override void Exit()
    {
        base.Exit();
  
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Core.Movement.SetVelocity(Input.x * PlayerData.MovementVelocity, Input.y * PlayerData.MovementVelocity);
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
        if (Core.Movement.FacingDirection == 1 && Input.x < 0)
        {
            Core.Movement.Flip();
        }
        else if (Core.Movement.FacingDirection == -1 && Input.x > 0)
        {
            Core.Movement.Flip();
        }
    }

}
