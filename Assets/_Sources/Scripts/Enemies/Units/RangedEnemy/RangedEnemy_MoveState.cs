using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;
using UnityEngine;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_MoveState : MoveState

    {
        private RangedEnemy _enemy;
        public RangedEnemy_MoveState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_MoveState stateData, RangedEnemy enemy) :
            base(entity, stateMachine, animBoolName, stateData)
        {
            _enemy = enemy;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _enemy.Core.Movement.SetVelocity(Direction, StateData.MovementSpeed);

            if (IsPlayerInMaxAgroRange)
            {
                //StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else if (IsDetectingWall)
            {
                SpendTime += Time.deltaTime;
                if (SpendTime >= TimeBeforeDetectingWall || Direction == Vector2.zero)
                {
                    StateMachine.ChangeState(_enemy.IdleState);
                }

            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }
}