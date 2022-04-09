using System;
using System.Collections.Generic;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts
{
    public class GameActions : SingletonClass<GameActions>
    {


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

        public event Action OnDungeonFinished;
        public void DungeonFinished()
        {
            if (OnDungeonFinished != null)
            {
                OnDungeonFinished();
            }
        }
        public event Action OnLevelChanged;
        public void RegenerateDungeon()
        {
            if (OnLevelChanged != null)
            {
                OnLevelChanged();
            }
        }
        
        public event Action<float, bool> OnShieldChange;
        public void ChangeShieldValue(float amount, bool reset)
        {
            if (OnShieldChange != null)
            {
                OnShieldChange(amount, reset);
            }
        }
        public event Action<float, bool> OnHealthChange;
        public void ChangeHealthValue(float amount, bool reset)
        {
            if (OnHealthChange != null)
            {
                OnHealthChange(amount, reset);
            }
        }
        public event Action<float, bool> OnEnergyChange;
        public void ChangeEnergyValue(float amount, bool reset)
        {
            if (OnEnergyChange != null)
            {
                OnEnergyChange(amount, reset);
            }
        }

        public event Action<Transform> OnPlayerKilled;

        public void PlayerKilled(Transform point)
        {
            if (OnPlayerKilled != null)
            {
                OnPlayerKilled(point);
            }
        }
    }
}