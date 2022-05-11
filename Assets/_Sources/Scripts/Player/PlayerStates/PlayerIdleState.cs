using _Sources.Scripts.Player.Data;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;

namespace _Sources.Scripts.Player.PlayerStates
{
    public class PlayerIdleState : PlayerCanCombatState
    {
        public PlayerIdleState(PlayerEntity playerEntity, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) :
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

            Core.Movement.SetVelocityZero();
        }

        public override void Exit()
        {
            base.Exit();
      
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(Input.x != 0f && Input.y != 0f)
            {
                StateMachine.ChangeState(PlayerEntity.MoveState);
            }
     
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
