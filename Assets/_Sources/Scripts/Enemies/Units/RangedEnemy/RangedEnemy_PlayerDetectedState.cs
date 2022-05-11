using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_PlayerDetectedState : PlayerDetectedState
    {
        private RangedEnemy _enemy;
        public RangedEnemy_PlayerDetectedState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PlayerDetectedState stateData, RangedEnemy enemy) : 
            base(entity, stateMachine, animBoolName, stateData)
        {
            _enemy = enemy;
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