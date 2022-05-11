using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class PlayerDetectedState : State
    {
        protected D_PlayerDetectedState StateData;

        protected bool IsDetectingWall;
        protected bool IsPlayerInMinAgroRange;
        protected bool IsPlayerInMaxAgroRange;

        protected bool PerformLongRangeAction;
        protected bool PerformCloseRangeAction;

        //protected Vector2 PlayerDetectedAt;
        public PlayerDetectedState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : 
            base(entity, stateMachine, animBoolName)
        {
            StateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
            IsPlayerInMaxAgroRange = Entity.Core.PlayerDetectionSenses.InMaxAgroRange();
            PerformCloseRangeAction = Entity.Core.PlayerDetectionSenses.InCloseRangeAction;
            IsDetectingWall = Entity.Core.CollisionSenses.Wall;

        }

        public override void Enter()
        {
            base.Enter();

            PerformLongRangeAction = false;
            //PlayerDetectedAt = Entity.GetPlayerPosition();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(Time.time >= StartTime + StateData.TimeBeforLongRangeAction)
            {
                PerformLongRangeAction = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
