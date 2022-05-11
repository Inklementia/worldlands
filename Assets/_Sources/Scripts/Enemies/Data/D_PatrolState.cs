using UnityEngine;

namespace _Sources.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newPatrolStateData", menuName = "Data/State Data/Patrol State Data")]
    public class D_PatrolState : ScriptableObject
    {
        public float MovementSpeed = 3f;
        public float PatrolDistance = 6f;
    }
}
