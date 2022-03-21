using System;
using System.Collections;
using _Sources.Scripts.Core;
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
       // [SerializeField] private StandaloneHealthSystem healthSystem;
       public EnemyBuildingCore Core { get; private set; }
        private float _fullRespawnTimer = 0.0f;
        private ObjectPooler _pooler;

        private void Awake()
        {
            Core = GetComponentInChildren<EnemyBuildingCore>();
            
            //healthSystem.SetMaxStat(enemySpawnerData.MaxHealth);
        }

        private void OnEnable()
        {
            
        }

        private void Start()
        {
            _pooler = ObjectPooler.Instance;
            
            Core.HealthSystem.IsDead = false;
            Core.Movement.SetFacingDirection(-1); //unnesseray
            Core.HealthSystem.SetMaxStat(enemySpawnerData.MaxHealth);
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
            StartCoroutine(SpawnMultiple());
       
        }

        private IEnumerator SpawnMultiple()
        {
            for (int i = 0; i < enemySpawnerData.NumberOfEnemies; i++)
            {
                //_pooler.SpawnFromPool(enemySpawnerData.SpawnEnemyTag, transform.position , Quaternion.identity);
                SpawnSingle();
                float randomSpawnInterval = Random.Range(0, enemySpawnerData.SpawnInterval);
                yield return new WaitForSeconds(randomSpawnInterval);

            }
        }
        private void SpawnSingle()
        {
            Vector3 randomOffset = new Vector3(Random.Range(0, 3),Random.Range(0, 3),Random.Range(0, 3));
            _pooler.SpawnFromPool(enemySpawnerData.SpawnEnemyTag, transform.position + randomOffset, Quaternion.identity);
            //yield return new WaitForSeconds(enemySpawnerData.SpawnInterval);
           
           
          
           
           
        }
        
    }
}