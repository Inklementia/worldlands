using _Sources.Scripts.Enemies.State_Mashine;

namespace _Sources.Scripts.Enemies.States
{
    public class SpawnState : State
    {
        protected bool IsAnimationFinished;
        protected bool IsPlayerInMinAgroRange;
        protected bool PerformCloseRangeAction;
            
        public SpawnState(Entity entity, FiniteStateMashine stateMachine, string animBoolName) : 
            base(entity, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Entity.AnimationToStateMachine.SpawnState = this;
            IsAnimationFinished = false;
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

        public override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
            PerformCloseRangeAction = Entity.Core.PlayerDetectionSenses.InCloseRangeAction;
        }
        
        public virtual void FinishSpawn()//when attack is done
        {
            IsAnimationFinished = true;
        }
        
    }
}