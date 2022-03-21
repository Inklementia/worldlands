using _Sources.Scripts.Core.Components;
using UnityEngine;

namespace _Sources.Scripts.Core
{
    public class EnemyBuildingCore : Core
    
    {
        public PlayerDetectionSenses PlayerDetectionSenses { get; private set; }

        protected override void Awake()
        {
            base.Awake();


            PlayerDetectionSenses = GetComponentInChildren<PlayerDetectionSenses>();

            if (!PlayerDetectionSenses)
            {
                Debug.LogError("Missing Senses Component");
            }

        }
    }
}