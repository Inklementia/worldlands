using System;
using System.Collections.Generic;
using System.Linq;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using _Sources.Scripts.Weapons.Weapon_Features;
using UnityEngine;

namespace _Sources.Scripts.Player
{
    public class AimHelper: MonoBehaviour
    {
        [SerializeField] private RotatableWeapon rotatableWeapon;
        [SerializeField] private GameObject indicatorPrefab;
        [SerializeField] private float detectEnemiesInterval = 5f;
        [SerializeField] private Tag enemyTag;
        [SerializeField] private float maxRange = 100;
        private float _detectEnemiesTimer = 0f;
        private HashSet<GameObject> _enemies;

        private List<GameObject> _enemiesList = new List<GameObject>();
        public GameObject ClosestEnemy { get; private set; }
        private PlayerEntity _playerEntity;
        private GameObject _indicator;
        private void Awake()
        {
           
            //GameActions.Current.OnDungeonGenerated += DetectEnemies;
            _indicator = GameObject.Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity);
            _indicator.SetActive(false);
        }

        private void Start()
        {
            _detectEnemiesTimer = detectEnemiesInterval;
            GameActions.Current.OnEnemyKilled += RemoveEnemyFromList;
            //GameActions.Current.OnBattleColliderEntered += DetectEnemies;
        }

        private void OnDisable()
        {
            GameActions.Current.OnEnemyKilled -= RemoveEnemyFromList;
           // GameActions.Current.OnBattleColliderEntered -= DetectEnemies;
            //GameActions.Current.OnDungeonGenerated -= DetectEnemies;
        }
        
        private void Update()
        {
            if(_playerEntity == null) return;
            if(!_playerEntity.Core.EnemyDetectionSenses.EnemyInFieldOfView()) return;
            
            _detectEnemiesTimer += Time.deltaTime;
            if (_detectEnemiesTimer >= detectEnemiesInterval)
            {
                DetectEnemies();
            }

        }

        public Vector2 GetDirection()
        {
            //Debug.Log("Enemy detects");
            Vector2 direction = ClosestEnemy.transform.position - _playerEntity.transform.position;
            direction.Normalize();
            return direction;

        }
        
        // TODO: event
        public void DetectEnemies()
        {
            _playerEntity.Core.EnemyDetectionSenses.DetectEnemiesInFieldOfView();
            _enemiesList = _playerEntity.Core.EnemyDetectionSenses.EnemiesList;
            FindClosestEnemy();
            
        }

        public void AssignPlayerToAimHelper(PlayerEntity player)
        {
              _playerEntity = rotatableWeapon.PlayerEntity;
        }
        private void RemoveEnemyFromList(GameObject enemyToRemove)
        {
            _enemiesList.Remove(enemyToRemove);
            FindClosestEnemy();
        }
        
        private void FindClosestEnemy()
        {
            if (_playerEntity != null)
            {
                float range = maxRange;
                foreach (GameObject enemyGO in _enemiesList)
                {
                    float dist = Vector2.Distance(enemyGO.transform.position, _playerEntity.transform.position);
                    if (dist < range)
                    {
                        range = dist;
                        ClosestEnemy = enemyGO;
                        ActivateIndicator();
                    }
                }
                
                _detectEnemiesTimer = 0.0f;
            }

        }

        private void ActivateIndicator()
        {
            _indicator.SetActive(true);
            _indicator.transform.parent = ClosestEnemy.transform;
            _indicator.transform.localPosition = Vector3.zero;
            
           
        }
        
        protected virtual void OnDrawGizmos()
        {
            //Core.EnemyDetectionSenses.FOV
            Gizmos.DrawWireSphere(transform.position, 6);
            
        }

    }
}