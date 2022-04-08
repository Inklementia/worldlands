using _Sources.Scripts.Core.Components;
using UnityEngine;

namespace _Sources.Scripts.Core
{
    public class EnemyBuildingCore : Core
    {
        public PlayerDetectionSenses PlayerDetectionSenses { get; private set; }
        public CombatSystem CombatSystem { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            CombatSystem = GetComponentInChildren<CombatSystem>();
            PlayerDetectionSenses = GetComponentInChildren<PlayerDetectionSenses>();

            if (!PlayerDetectionSenses || !CombatSystem)
            {
                Debug.LogError("Missing Senses Component");
            }

        }
    }
}