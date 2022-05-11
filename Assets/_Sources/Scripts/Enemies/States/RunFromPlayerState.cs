using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;

namespace _Sources.Scripts.Enemies.States
{
    public class RunFromPlayerState : State
    {
        protected D_MoveState StateData;
        public RunFromPlayerState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_MoveState stateData) : 
            base(entity, stateMachine, animBoolName)
        {
            StateData = stateData;
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
    }
}