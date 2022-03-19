using _Sources.Scripts.Core.Components;
using UnityEngine;

namespace _Sources.Scripts.Core
{
    public abstract class Core : MonoBehaviour
    {
        public Movement Movement { get; private set; }
        public HealthSystem HealthSystem { get; private set; }
        
        public CombatSystem CombatSystem { get; private set; }

        protected virtual void Awake()
        {
            Movement = GetComponentInChildren<Movement>();
            HealthSystem = GetComponentInChildren<HealthSystem>();
            CombatSystem = GetComponentInChildren<CombatSystem>();
            if (!Movement)
            {
                Debug.LogError("Missing Movement Component");
            }
            if (!HealthSystem)
            {
                Debug.LogError("Missing HealthSystem Component");
            }
            if (!HealthSystem)
            {
                Debug.LogError("Missing CombatSystem Component");
            }
        }

        public virtual  void LogicUpdate()
        {
            CombatSystem.LogicUpdate();
            
        }
    }
}
