using _Sources.Scripts.Enemies.States;
using _Sources.Scripts.Player.Data;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Player.PlayerStates
{
    public class PlayerCanCombatState : PlayerState
    {
        protected Vector2 Input;

        public PlayerCanCombatState(PlayerEntity playerEntity, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) :
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
            if (PlayerEntity.InputHandler.CkeckIfJoystickPressed())
            {
                Input.x = PlayerEntity.InputHandler.MovementPos.x;
                Input.y = PlayerEntity.InputHandler.MovementPos.y;
            }
            else
            {
                Input.x = 0;
                Input.y = 0;
            }

                          
      
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
