using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts;
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
        if (Player.InputHandler.CkeckIfJoystickPressed())
        {
            Input.x = Player.InputHandler.MovementPosX;
            Input.y = Player.InputHandler.MovementPosY;
        }
        else
        {
            Input.x = 0;
            Input.y = 0;
        }

        // if health < 0  -> DeathState
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
