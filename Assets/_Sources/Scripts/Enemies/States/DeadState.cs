using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Helpers;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class DeadState : State
    {
        protected D_DeadState StateData;
    
    
        public DeadState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_DeadState stateData) : 
            base(entity, stateMachine, animBoolName)
        {
            StateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();

            //ObjectPooler.Instance.SpawnFromPool(StateData.DeathParticlesTag, Entity.transform.position,
            //    Entity.transform.rotation);
            //ObjectPooler.Instance.SpawnFromPool(StateData.DeathChunkParticlesTag, Entity.transform.position,Entity.transform.rotation);
            Entity.Dead.transform.position = Entity.Alive.transform.position;
            Entity.Dead.gameObject.SetActive(true);
            Entity.Alive.gameObject.SetActive(false);
            GameActions.Instance.EnemyKilledTrigger(Entity.gameObject);
     
            Debug.Log("Enemy Killed");
      
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
    

