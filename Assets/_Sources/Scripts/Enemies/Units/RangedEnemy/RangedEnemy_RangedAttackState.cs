using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;
using _Sources.Scripts.Weapons;
using UnityEngine;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_RangedAttackState : RangedAttackState
    {
         private RangedEnemy _enemy;

         public RangedEnemy_RangedAttackState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, ShootingWeapon shootingWeapon, RangedEnemy enemy) :
             base(entity, stateMachine, animBoolName, attackPosition, stateData, shootingWeapon)
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
             
             if (IsAnimationFinished)
             {
                 if (InMinAgroRange)
                 {
                     //_enemy.Core.Movement
                 }
                 else
                 {
                     //_stateMachine.ChangeState(_enemy._lookForPlayerState);
                 }
             }
         }

         public override void PhysicsUpdate()
         {
             base.PhysicsUpdate();
         }

         public override void TriggerAttack()
         {
             base.TriggerAttack();
         }

         public override void FinishAttack()
         {
             base.FinishAttack();
         }
    }
}