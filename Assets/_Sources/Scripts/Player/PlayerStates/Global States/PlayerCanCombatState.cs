using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanCombatState : PlayerState
{
    protected Vector2 Input;

    public PlayerCanCombatState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) :
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
        Input.x = Player.InputHandler.MovementPosX;
        Input.y = Player.InputHandler.MovementPosY;

        // if health < 0  -> DeathState
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
