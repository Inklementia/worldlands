using System;
using UnityEngine;

namespace _Sources.Scripts.Battle
{
    public partial class BattleSystem : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
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
            colliderTrigger.OnEnterTrigger += OnPlayerEnterRoom;
        }
        
        private void OnPlayerEnterRoom(object sender, EventArgs e)
        {
            if (_currentBattleState == BattleState.Idle)
            {
                StartBattle();
                colliderTrigger.OnEnterTrigger -= OnPlayerEnterRoom;
            }
            
        }

        private void StartBattle()
        {
            enemySpawner.ActivateSpawner();
        }
    }
}