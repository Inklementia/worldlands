﻿using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Player.PlayerStates
{
    public class PlayerDeadState : PlayerState
    {
        protected bool IsAnimationFinished; 
        public PlayerDeadState(PlayerEntity playerEntity, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) : 
            base(playerEntity, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerEntity.Core.Movement.SetVelocityZero();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (IsAnimationFinished)
            {
                //does not work 
                Debug.Log("Player died!");
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public virtual void FinishAnimation()
        {
            IsAnimationFinished = true;
        }
    }
}