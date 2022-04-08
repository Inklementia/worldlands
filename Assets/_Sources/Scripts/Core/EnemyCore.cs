using _Sources.Scripts.Core.Components;
using UnityEngine;

namespace _Sources.Scripts.Core
{
    public class EnemyCore : Core
    {
        
        public CollisionSenses CollisionSenses { get; private set; }
        public PlayerDetectionSenses PlayerDetectionSenses { get; private set; }
        public CombatSystem CombatSystem { get; private set; }
        protected override void Awake()
        {
            base.Awake();

            CollisionSenses = GetComponentInChildren<CollisionSenses>();
            PlayerDetectionSenses = GetComponentInChildren<PlayerDetectionSenses>();
            CombatSystem = GetComponentInChildren<CombatSystem>();

            if (!CollisionSenses || !PlayerDetectionSenses)
            {
                Debug.LogError("Missing Senses Component");
            }
            if (!CombatSystem)
            {
                Debug.LogError("Missing Combat Component");
            }

        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            PlayerDetectionSenses.LogicUpdate();
            CombatSystem.LogicUpdate();
        }
    }
}
