using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;
using UnityEngine;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_MeleeAttackState : MeleeAttackState
    {
        private RangedEnemy _enemy;
        public RangedEnemy_MeleeAttackState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, RangedEnemy enemy) :
            base(entity, stateMachine, animBoolName, attackPosition, stateData)
        {
            _enemy = enemy;
        }
    }
}