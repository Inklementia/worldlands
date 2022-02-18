using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;
using UnityEngine;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_RangedAttackState : RangedAttackState
    {
         private RangedEnemy _enemy;
        public RangedEnemy_RangedAttackState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, RangedEnemy enemy) : 
            base(entity, stateMachine, animBoolName, attackPosition, stateData)
        {
            _enemy = enemy;
        }
    }
}