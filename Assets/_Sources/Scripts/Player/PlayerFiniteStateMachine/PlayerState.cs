using _Sources.Scripts.Core;
using _Sources.Scripts.Player.Data;
using UnityEngine;

namespace _Sources.Scripts.Player.PlayerFiniteStateMachine
{
    public class PlayerState
    {
        protected PlayerCore Core;

        protected PlayerEntity PlayerEntity;
        protected PlayerStateMachine StateMachine;
        protected PlayerDataSO PlayerData;

        protected float StartTime;
  
        private string _animBoolName;

        public PlayerState(PlayerEntity playerEntity, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName)
        {
            PlayerEntity = playerEntity;
            StateMachine = stateMachine;
            PlayerData = playerData;
            _animBoolName = animBoolName;
            Core = playerEntity.Core;
        }

        public virtual void Enter()
        {
            DoChecks();
            StartTime = Time.time;
            PlayerEntity.Anim.SetBool(_animBoolName, true);
        }
        public virtual void Exit()
        {
            PlayerEntity.Anim.SetBool(_animBoolName, false);
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
}
