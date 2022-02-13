using _Sources.Scripts.Core.Components;
using UnityEngine;

namespace _Sources.Scripts.Core
{
    public abstract class Core : MonoBehaviour
    {
        public Movement Movement { get; private set; }
        public HealthSystem HealthSystem { get; private set; }

        protected virtual void Awake()
        {
            Movement = GetComponentInChildren<Movement>();
            HealthSystem = GetComponentInChildren<HealthSystem>();

            if (!Movement)
            {
                Debug.LogError("Missing Movement Component");
            }
            if (!HealthSystem)
            {
                Debug.LogError("Missing HealthSystem Component");
            }
        }

        //public void LogicUpdate()
        //{
        //    Movement.LogicUpdate();
        //}
    }
}
