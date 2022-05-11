using UnityEngine;

namespace _Sources.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/PlayerDetected State Data")]
    public class D_PlayerDetectedState : ScriptableObject
    {
        public float TimeBeforLongRangeAction = 1.5f;
    }
}
