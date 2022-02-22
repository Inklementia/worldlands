using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Weapons;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class RangedAttackState : AttackState
    {
        protected D_RangedAttackState StateData;

        protected AttackDetails AttackDetails;

        protected bool InMaxAgroRange;
        protected bool InMinAgroRange;
        protected bool PerformCloseRangeAction;

        protected ShootingWeapon ShootingWeapon;
        
        public RangedAttackState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, ShootingWeapon shootingWeapon) :
            base(entity, stateMachine, animBoolName, attackPosition)
        {
            StateData = stateData;
            ShootingWeapon = shootingWeapon;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            InMaxAgroRange = Entity.Core.PlayerDetectionSenses.InMaxAgroRange;
            InMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
            PerformCloseRangeAction = Entity.Core.PlayerDetectionSenses.InCloseRangeAction;
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

        public override void TriggerAttack()
        {
            base.TriggerAttack();
            
            ShootingWeapon.Attack();
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}