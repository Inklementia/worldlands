using _Sources.Scripts.Player;
using _Sources.Scripts.Player.Data;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Player.PlayerStates
{
    public class PlayerMoveState : PlayerCanCombatState
    {
        public PlayerMoveState(PlayerEntity playerEntity, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) :
            base(playerEntity, stateMachine, playerData, animBoolName)
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
                StateMachine.ChangeState(PlayerEntity.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void CheckMovementDirection()
        {
            if (!Core.EnemyDetectionSenses.EnemyInFieldOfView() ||
                PlayerEntity.WeaponHandler.Weaponry.CurrentWeapon == null
               )
            {
                //if player detect enemies he doesn not flip
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

    }
}
