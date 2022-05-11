using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using _Sources.Scripts.Structs;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class MeleeAttackState : AttackState
    {
        protected D_MeleeAttackState StateData;

        protected AttackDetails AttackDetails;

        protected bool PerformCloseRangeAction;

        public MeleeAttackState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : 
            base(entity, stateMachine, animBoolName, attackPosition)
        {
            StateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();

            PerformCloseRangeAction = Entity.Core.PlayerDetectionSenses.InCloseRangeAction;
       
        }

        public override void Enter()
        {
            base.Enter();

            AttackDetails.DamageAmount = StateData.AttackDamage;
            AttackDetails.Position = Entity.transform.position;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
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

            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position, StateData.AttackRadius, StateData.WhatIsPlayer);

            foreach (Collider2D collider in detectedObjects)
            {
                PlayerEntity player = collider.GetComponent<PlayerEntity>();
                player.CheckDamage();
                player.Core.PlayerCombatSystem.TakeDamage(AttackDetails);
                Debug.Log("Attack");
            }
        }
    
    }
}
