using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy_DeadState : DeadState
    {
        private RangedEnemy _enemy;
        public RangedEnemy_DeadState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_DeadState stateData, RangedEnemy enemy) : 
            base(entity, stateMachine, animBoolName, stateData)
        {
            _enemy = enemy;
        }
    }
}