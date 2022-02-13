using _Sources.Scripts.Core.Components;
using UnityEngine;

namespace _Sources.Scripts.Core
{
    public class PlayerCore : Core
    {
        public EnemyDetectionSenses EnemyDetectionSenses { get; private set; }

        protected override void Awake()
        {
            base.Awake();

     
            EnemyDetectionSenses = GetComponentInChildren<EnemyDetectionSenses>();

            if (!EnemyDetectionSenses)
            {
                Debug.LogError("Missing Senses Component");
            }

        }
    }
}
