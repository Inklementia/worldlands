using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Object_Pooler;
using DG.Tweening;
using UnityEngine;

namespace _Sources.Scripts.Enemies.States
{
    public class DamageState : State
    {
        protected D_DamageState StateData;

        protected bool IsDamageTimeOver;

        protected bool IsPlayerInMinAgroRange;

        protected Material CurrentMaterial;

        public DamageState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_DamageState stateData) : base(entity, stateMachine, animBoolName)
        {
            StateData = stateData;
            CurrentMaterial = new Material(StateData.HitMaterial);
            entity.Alive.GetComponent<SpriteRenderer>().material = CurrentMaterial;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = Entity.Core.PlayerDetectionSenses.InMinAgroRange;
        }

        public override void Enter()
        {
            base.Enter();
            IsDamageTimeOver = false;

            Sequence hitSFXsequence = DOTween.Sequence();
            hitSFXsequence.Append( CurrentMaterial.DOFloat(.8f, "_HitEffectBlend", .1f));
            hitSFXsequence.Append( CurrentMaterial.DOFloat(0f, "_HitEffectBlend", .1f));  
       
           
            ObjectPooler.Instance.SpawnFromPool(StateData.HitParticles, Entity.Alive.transform.position,
                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            
      
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + StateData.DamageTime)
            {
                IsDamageTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
