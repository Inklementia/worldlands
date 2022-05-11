using _Sources.Scripts.Player.Data;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using _Sources.Scripts.Weapons;

namespace _Sources.Scripts.Player.PlayerStates
{
    public class PlayerAttackState : PlayerState
    {
        private ShootingWeapon _weapon;

        public PlayerAttackState(PlayerEntity playerEntity, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animBoolName) : base(playerEntity, stateMachine, playerData, animBoolName)
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


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
