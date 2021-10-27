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

    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        PlayerData = playerData;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        Player.Anim.SetBool(_animBoolName, true);
        StartTime = Time.time;
        Debug.Log(_animBoolName);
    }
    public virtual void Exit()
    {
        Player.Anim.SetBool(_animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
        Input.x = Player.InputHandler._movementPosX;
        Input.y = Player.InputHandler._movementPosY;
    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}
