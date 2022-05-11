using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_DamageState : DamageState
    {
        private RangedEnemy _enemy;
        public RangedEnemy_DamageState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_DamageState stateData, RangedEnemy enemy) : 
            base(entity, stateMachine, animBoolName, stateData)
        {
            _enemy = enemy;
        }
    }
}