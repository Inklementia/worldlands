using System;
using System.Collections.Generic;
using System.Linq;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using Dungeon;
using UnityEngine;

namespace _Sources.Scripts.Weapons.Weapon_Features
{
    public class RotatableWeapon : MonoBehaviour, IWeaponFeature
    {
        [SerializeField] private bool rotateOnAttack;
        [SerializeField] private WeaponTypeDataSO rotatableWeaponData;
        [SerializeField] private float initialAngle = 90;
        [SerializeField] private float fovRange = 6;
        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private float maxRange = 100;
        [SerializeField] private Tag enemyTag;
        //[SerializeField] private DungeonManager dm;
        [SerializeField] private float detectEnemiesInterval = 5f;
        
        private List<GameObject> _enemiesList = new List<GameObject>();
        private GameObject _closestEnemy;
        
        public float RotateAngle { get; private set; }
        public float InitialRotateAngle { get; private set; }

        private PlayerEntity _playerEntity;
        public WeaponTypeDataSO RotatableWeaponData { get => rotatableWeaponData; private set => rotatableWeaponData = value; }

        private float _detectEnemiesTimer = 0.0f;
        private HashSet<GameObject> _enemies;
        

        private void Awake()
        {
            InitialRotateAngle = initialAngle;
            
            _enemies = gameObject.FindAllWithTag(enemyTag);
        }

        private void Start()
        {
            GameActions.Current.OnEnemyKilled += RemoveEnemyFromList;
            GameActions.Current.OnDungeonGenerated += DetectEnemies;
        }

        private void OnDisable()
        {
            GameActions.Current.OnEnemyKilled -= RemoveEnemyFromList;
            GameActions.Current.OnDungeonGenerated -= DetectEnemies;
        }



        private void Update()
        {
            _detectEnemiesTimer += Time.deltaTime;
            if (_detectEnemiesTimer >= detectEnemiesInterval)
            {
                FindClosestEnemy();
            }
        }

        private void FixedUpdate()
        {
            //Debug.Log(InitialRotateAngle);
            if (_playerEntity != null)
            {
                if (_playerEntity.Core.EnemyDetectionSenses.EnemyInFieldOfView())
                {
                    Vector2 direction =  _closestEnemy.transform.position - _playerEntity.transform.position;
                    direction.Normalize();
                    
                    RotateWeapon(direction);
                }
                else
                {
                    if (rotateOnAttack && _playerEntity.InputHandler.IsAttackButtonPressedDown && _playerEntity.InputHandler.CkeckIfJoystickPressed())
                    {
                        RotateWeapon(_playerEntity.InputHandler.MovementPos);
                    }
                    if (_playerEntity.InputHandler.CkeckIfJoystickPressed() && !rotateOnAttack)
                    {
                        RotateWeapon(_playerEntity.InputHandler.MovementPos);
                    }
                }
               
            }
        }

        private void RotateWeapon(Vector2 where)
        {
          
            RotateAngle = Mathf.Atan2(where.x, where.y) * Mathf.Rad2Deg;
            InitialRotateAngle = Mathf.Atan2(where.x, where.y) * Mathf.Rad2Deg;
            if (RotateAngle < 0)
            {
                RotateAngle = -RotateAngle;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, -(RotateAngle - initialAngle));
                
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -(RotateAngle - initialAngle));
               
            }
        }
        
        
        
        public void Accept(IVisitor visitor)
        {
            _playerEntity = gameObject.FindWithTag(RotatableWeaponData.PlayerTag).GetComponent<Player.PlayerFiniteStateMachine.PlayerEntity>();
            
            //_enemiesList = dm.SpawnedEnemies;
            //DetectEnemies();
       
            visitor.Visit(this);

            //_player.WeaponHandler.Weaponry.CurrentWeapon.Accept(rotatable)
        }

        // TODO: event
        private void DetectEnemies()
        {
            _enemiesList = _enemies.Any() ? _enemies.ToList() : null;
            FindClosestEnemy();
            
        }
        private void RemoveEnemyFromList(GameObject enemyToRemove)
        {
            _enemiesList.Remove(enemyToRemove);
            FindClosestEnemy();
        }
        public void UnsetPlayer()
        { 
            _playerEntity = null;
        }
        
 
        protected virtual void OnDrawGizmos()
        {
            if (_playerEntity != null)
            {
                Gizmos.DrawWireSphere(_playerEntity.transform.position, fovRange);
            }
        }

        private void FindClosestEnemy()
        {
            if (_playerEntity != null && _enemiesList.Count > 0)
            {
                float range = maxRange;
                foreach (GameObject enemyGO in _enemiesList)
                {
                    float dist = Vector2.Distance(enemyGO.transform.position, _playerEntity.transform.position);
                    if (dist < range)
                    {
                        range = dist;
                        _closestEnemy = enemyGO;
                    }
                }
                
                _detectEnemiesTimer = 0.0f;
            }

        }
    }
}
