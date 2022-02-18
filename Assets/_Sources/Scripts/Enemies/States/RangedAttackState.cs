using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class RangedAttackState : AttackState
    {
        protected D_RangedAttackState StateData;

        protected AttackDetails AttackDetails;

        protected bool PerformCloseRangeAction;
        
        public RangedAttackState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData) :
            base(entity, stateMachine, animBoolName, attackPosition)
        {
            StateData = stateData;
        }
    }
}