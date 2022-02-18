using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_IdleState : IdleState
    {

        private RangedEnemy _enemy;
        
        public RangedEnemy_IdleState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_IdleState stateData, RangedEnemy enemy) : 
            base(entity, stateMachine, animBoolName, stateData)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.StopMovement();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (IsPlayerInMaxAgroRange)
            {
                //StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else if (IsIdleTimeOver)
            {
                StateMachine.ChangeState(_enemy.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}