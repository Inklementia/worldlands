using _Sources.Scripts.Object_Pooler;
using _Sources.Scripts.Player.Data;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Player.PlayerStates
{
    public class PlayerDamageState : PlayerCanCombatState
    {
        private bool _isDamageTimeOver;
        public PlayerDamageState(PlayerEntity playerEntity, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) : 
            base(playerEntity, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _isDamageTimeOver = false;

            ObjectPooler.Instance.SpawnFromPool(PlayerData.HitParticlesTag, PlayerEntity.transform.position,
                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time >= StartTime + PlayerData.DamageTime)
            {
                _isDamageTimeOver = true;
            }
            if (_isDamageTimeOver)
            {
                if(Input.x != 0f && Input.y != 0f)
                {
                    StateMachine.ChangeState(PlayerEntity.MoveState);
                }
                else
                {
                    StateMachine.ChangeState(PlayerEntity.IdleState);
                }
                
            }
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}