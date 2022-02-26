using System;
using System.Collections;
using _Sources.Scripts.Core.Components;
using _Sources.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

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
            healthSystem.SetMaxStat(enemySpawnerData.MaxHealth);
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
            healthSystem.DecreaseStat(attackDetails.DamageAmount);
            if (enemySpawnerData.SpawnOnHit)
            {
                SpawnEnemies();
            }
        }
   
      
        public void SpawnEnemies()
        {
            float effectDuration = 0.3f;
            float shakeStrength = .3f;
            int shakeVibrato = 20;
            float shakeRandomness = 0;


        //DOShakeRotation(float duration, float / Vector3 strength, int vibrato, float randomness, bool fadeOut)
        transform.DOShakePosition(effectDuration, new Vector3(.3f, .1f, 0), shakeVibrato, shakeRandomness).SetLoops(1, LoopType.Restart);
            //transform.DOShakeScale(effectDuration, new Vector3(shakeStrength, shakeStrength, shakeStrength), shakeVibrato, shakeRandomness).SetLoops(1, LoopType.Restart);
            //transform.DOShakeRotation(effectDuration, new Vector3(0, 0, 20f), shakeVibrato, shakeRandomness).SetLoops(1, LoopType.Restart);

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