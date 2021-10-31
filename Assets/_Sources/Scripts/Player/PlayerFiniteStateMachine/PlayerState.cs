using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core Core;

    protected Player Player;
    protected PlayerStateMachine StateMachine;
    protected PlayerDataSO PlayerData;

    protected float StartTime;
  
    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        PlayerData = playerData;
        _animBoolName = animBoolName;
        Core = player.Core;
    }

    public virtual void Enter()
    {
        DoChecks();
        StartTime = Time.time;
        Player.Anim.SetBool(_animBoolName, true);
    }
    public virtual void Exit()
    {
        Player.Anim.SetBool(_animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
 
    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}
