using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

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

        ObjectPooler.Instance.SpawnFromPool(StateData.DeathBloodParticlesTag, Entity.transform.position,
                   Entity.transform.rotation);
        //ObjectPooler.Instance.SpawnFromPool(StateData.DeathChunkParticlesTag, Entity.transform.position,Entity.transform.rotation);

        Entity.gameObject.SetActive(false);
      
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
    

