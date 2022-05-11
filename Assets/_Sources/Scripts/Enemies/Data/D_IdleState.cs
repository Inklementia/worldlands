using UnityEngine;

namespace _Sources.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State Data")]
    public class D_IdleState : ScriptableObject
    {
        public float MinIdleTime = 1f;
        public float MaxIdleTime = 3f;
    }
}
