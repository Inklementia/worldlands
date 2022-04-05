using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class PatrolState : State
    {
        // state for patroling certain area
        protected D_PatrolState StateData;

        protected Vector3 MovePos;
        protected bool IsPlayerInMinAgroRange;
        protected bool IsPlayerInMaxAgroRange;
        protected bool IsDetectingWall;

        public PatrolState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PatrolState stateData) : 
            base(entity, stateMachine, animBoolName)
        {
            StateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();

            IsDetectingWall = Entity.Core.CollisionSenses.Wall;
            IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
            IsPlayerInMaxAgroRange = Entity.Core.PlayerDetectionSenses.InMaxAgroRange();
        }

        public override void Enter()
        {
            base.Enter();
            // set velocity or transform
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

        protected void ChangeMoveDirection()
        {
            //MovePos = new Vector3(
            //         Random.Range(Entity.StartingPos.x - StateData.PatrolDistance - 1, Entity.StartingPos.x + StateData.PatrolDistance - 1),
            //         Random.Range(Entity.StartingPos.y - StateData.PatrolDistance - 1, Entity.StartingPos.y + StateData.PatrolDistance - 1), 0f
            //         );
            //if (Entity.MoveTarget.GetComponent<CheckIfObstacle>().HasObstacleOnWay)
            //{
            //    ChangeMoveDirection();
            //}
            //else
            //{
            //    Entity.MoveTarget.position = MovePos;
            //}

            //var point = Random.insideUnitSphere * 20;
            //point.y = 0;
            //point += ai.position;
            //Entity.MoveTarget.position = point;
            //return point;

        }

        protected void HandleFlip()
        {
            if (MovePos.x > Entity.transform.position.x)
            {
                Entity.Core.Movement.Flip180();
            }
            else if(MovePos.x < Entity.transform.position.x)
            {
                Entity.Core.Movement.Flip0();
            }
        }
    }
}
