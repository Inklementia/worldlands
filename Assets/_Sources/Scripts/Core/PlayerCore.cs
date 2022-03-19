using System;
using _Sources.Scripts.Core.Components;
using UnityEngine;

namespace _Sources.Scripts.Core
{
    public class PlayerCore : Core
    {
        public EnemyDetectionSenses EnemyDetectionSenses { get; private set; }
        public EnergySystem EnergySystem { get; private set; }
        public ShieldSystem ShieldSystem { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            EnemyDetectionSenses = GetComponentInChildren<EnemyDetectionSenses>();
            EnergySystem = GetComponentInChildren<EnergySystem>();
            ShieldSystem = GetComponentInChildren<ShieldSystem>();

            if (!EnemyDetectionSenses)
            {
                Debug.LogError("Missing Senses Component");
            }

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            //EnemyDetectionSenses.LogicUpdate();
            
        }
    }
}
