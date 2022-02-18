using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class DamageState : State
    {
        protected D_DamageState StateData;

        protected bool IsDamageTimeOver;

        protected bool IsPlayerInMinAgroRange;

        public DamageState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_DamageState stateData) : base(entity, stateMachine, animBoolName)
        {
            StateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
        }

        public override void Enter()
        {
            base.Enter();
            IsDamageTimeOver = false;

            ObjectPooler.Instance.SpawnFromPool(StateData.HitParticles, Entity.transform.position,
                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

      
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + StateData.DamageTime)
            {
                IsDamageTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
