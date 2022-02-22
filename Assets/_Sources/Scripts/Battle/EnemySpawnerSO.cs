using UnityEngine;

namespace _Sources.Scripts.Battle
{
    [CreateAssetMenu(fileName = "newEnemySpawnerData", menuName = "Data/Enemies Data/Enemy Spawner Data")]
    public class EnemySpawnerSO : ScriptableObject
    {
        public string Name = "Unknown Nest";
        public float MaxHealth = 500f;
        public float SpawnInterval = 2f;
        public float FullRespawnInterval = 10f;
        public bool SpawnOnHit;
        
        [Header("Units")]
        public int NumberOfEnemies;
        public Tag SpawnEnemyTag;

        [Header("Special Units")]
        public bool HasSpecialUnits;
        public int NumberOfSpecialEnemies;
        public Tag SpawnSpecialEnemyTag;
        
        
    }
}