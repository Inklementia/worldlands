using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player Player;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;

    protected float StartTime;
    protected Vector2 Input;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData)
    {
        Player = player;
        StateMachine = stateMachine;
        PlayerData = playerData;
       
    }

    public virtual void Enter()
    {
        DoChecks();
        StartTime = Time.time;
       
    }
    public virtual void Exit()
    {
       
    }
    public virtual void LogicUpdate()
    {
        Input.x = Player.InputHandler.MovementPosX;
        Input.y = Player.InputHandler.MovementPosY;
    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}
