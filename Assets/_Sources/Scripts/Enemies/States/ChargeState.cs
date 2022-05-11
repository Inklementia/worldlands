using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using Pathfinding;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class ChargeState : State
    {
        protected D_ChargeState StateData;

        protected Vector3 WhereToCharge;

        protected bool IsPlayerInMinAgroRange;
        protected bool IsPlayerInMaxAgroRange;
        protected bool IsDetectingWall;
        protected bool PerformCloseRangeAction;

        protected bool IsChargeTimeOver;

        // A*
        protected float NextWayPointDistance = 1f;
        protected Path Path;
        protected int CurrentWayPoint = 0;
        protected bool ReachedEndOfPath = false;

        public ChargeState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
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
            WhereToCharge = Core.PlayerDetectionSenses.Player.transform.position;
            IsChargeTimeOver = false;
            HandleFlip();
            //Entity.GoTo(Entity.GetTargetPosition(), StateData.ChargeSpeed);
            Entity.Seeker.StartPath(Entity.Rb.position, Entity.GetTargetPosition(), OnPathComplete);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

     

            if (Time.time >= StartTime + StateData.ChargeTime)
            {
                IsChargeTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            HandlePath();
        }

        private void HandleFlip()
        {
            if (WhereToCharge.x > Entity.transform.position.x)
            {
                Entity.Core.Movement.Flip180();
            }
            else if (WhereToCharge.x < Entity.transform.position.x)
            {
                Entity.Core.Movement.Flip0();
            }
        }
        private void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                Path = path;
                CurrentWayPoint = 0;
            
            }
        }

        private void UpdatePath()
        {
            if (Entity.Seeker.IsDone())
            {
                Debug.Log("Update Path working");
                Entity.Seeker.StartPath(Entity.Rb.position, Entity.GetTargetPosition(), OnPathComplete);
            }
        }

        private void HandlePath()
        {
            if (Path == null)
            {
                return;
            }
            if (CurrentWayPoint >= Path.vectorPath.Count)
            {
                ReachedEndOfPath = true;
                return;
            }
            else
            {
                ReachedEndOfPath = false;
            }
            Vector2 direction = ((Vector2)Path.vectorPath[CurrentWayPoint] - Entity.Rb.position).normalized;
            Entity.Core.Movement.SetVelocity(direction, StateData.ChargeSpeed);

            float distance = Vector2.Distance(Entity.Rb.position, Path.vectorPath[CurrentWayPoint]);

            if (distance < NextWayPointDistance)
            {
                CurrentWayPoint++;
            }
        }

        //public void SetWhereToCharge(Vector2 target)
        //{
        //    WhereToCharge = target;
        //}


    }
}