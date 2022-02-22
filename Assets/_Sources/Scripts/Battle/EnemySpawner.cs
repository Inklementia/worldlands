using System;
using System.Collections;
using _Sources.Scripts.Core.Components;
using _Sources.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Battle
{
    public class EnemySpawner : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemySpawnerSO enemySpawnerData;
        [SerializeField] private StandaloneHealthSystem healthSystem;

        private float _fullRespawnTimer = 0.0f;
        private ObjectPooler _pooler;

        private void Awake()
        {
            _pooler = ObjectPooler.Instance;
            healthSystem.SetMaxHealth(enemySpawnerData.MaxHealth);
        }

        private void Update()
        {
 
        }

        public void ActivateSpawner()
        {
            if (!enemySpawnerData.SpawnOnHit)
            {
                SpawnEnemies();
            }
        }

        private void RespawnNest()
        {
            if (_fullRespawnTimer >= enemySpawnerData.FullRespawnInterval)
            {
                SpawnEnemies();
            }
        }

        public void TakeDamage(AttackDetails attackDetails)
        {
            healthSystem.DecreaseHealth(attackDetails.DamageAmount);
            if (enemySpawnerData.SpawnOnHit)
            {
                SpawnEnemies();
            }
        }

        public void SpawnEnemies()
        {
            for (int i = 0; i < enemySpawnerData.NumberOfEnemies; i++)
            {
                StartCoroutine(SpawnSingle());

            }
        }

        private IEnumerator SpawnSingle()
        {
            Vector3 randomOffset = new Vector3(Random.Range(0, 3),Random.Range(0, 3),Random.Range(0, 3));
            GameObject enemyGO = _pooler.SpawnFromPool(enemySpawnerData.SpawnEnemyTag, transform.position + randomOffset, Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnerData.SpawnInterval);
        }
        
    }
}