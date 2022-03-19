using System;
using System.Collections.Generic;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts
{
    public class GameActions : MonoBehaviour
    {
        public static GameActions Current;

        private void Awake()
        {
            Current = this;
        }

        public event Action OnDungeonGenerated;

        public void DungeonGenerated()
        {
            if (OnDungeonGenerated != null)
            {
                OnDungeonGenerated();
            }
        }
        public event Action<List<Vector2>> OnDungeonGeneratedToSaveMap;

        public void DungeonGeneratedToSaveMap(List<Vector2> tiles)
        {
            if (OnDungeonGeneratedToSaveMap != null)
            {
                OnDungeonGeneratedToSaveMap(tiles);
            }
        }
        public event Action<List<Vector2>> OnLevelLoaded;

        public void LevelLoaded(List<Vector2> tiles)
        {
            if (OnLevelLoaded != null)
            {
                OnLevelLoaded(tiles);
            }
        }

        public event Action<GameObject> OnEnemyKilled;

        public void EnemyKilledTrigger(GameObject enemyGo)
        {
            if (OnEnemyKilled != null)
            {
                OnEnemyKilled(enemyGo);
            }
        }
        
        public event Action OnBattleColliderEntered;

        public void BattleColliderEntered()
        {
            if (OnBattleColliderEntered != null)
            {
                OnBattleColliderEntered();
            }
        }
    }
}