using System;
using UnityEngine;

namespace _Sources.Scripts.Battle
{
    public partial class BattleSystem : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        [SerializeField] private ColliderTrigger colliderTrigger;

        private BattleState _currentBattleState;
        
        private void Awake()
        {
           
            _currentBattleState = BattleState.Idle;
            //enemySpawner = GetComponent<EnemySpawner>();
            // register enemy spawner 
        }

        private void Start()
        {
            _enemySpawner = GetComponentInChildren<EnemySpawner>();
            colliderTrigger.OnEnterTrigger += OnPlayerEnterRoom;
            
        }
        
        private void OnPlayerEnterRoom(object sender, EventArgs e)
        {
            if (_currentBattleState == BattleState.Idle)
            {
                StartBattle();
                colliderTrigger.OnEnterTrigger -= OnPlayerEnterRoom;
                GameActions.Current.BattleColliderEntered();
            }
            
        }

        private void StartBattle()
        {
            _enemySpawner.ActivateSpawner();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}