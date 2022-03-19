using System;
using System.Collections.Generic;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Sources.Scripts
{
    public class WorldManager : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private WorldDataSO worldData;
        [SerializeField] private GameObject playerSpawnPoint;
        
        private int _currentLevel = 1;
        private int _currentWorld = 1;
        private bool _isBossScene = false;
        
        private List<Vector2> _currentMap;

        private void SetPlayerAtStartPosition(Vector3 place)
        {
            playerSpawnPoint.transform.position = place;
        }

        private void OnEnable()
        {
            GameActions.Current.OnDungeonGeneratedToSaveMap += SaveCurrentMap;
        }

        private void OnDisable()
        {
            GameActions.Current.OnDungeonGeneratedToSaveMap -= SaveCurrentMap;
        }

        private void SaveCurrentMap(List<Vector2> mapTiles)
        {
            _currentMap = mapTiles;
        }
        public void LoadProgress(PlayerProgress progress)
        {
            if (_currentWorld == progress.WorldData.LevelMap.WorldNumber &&
                _currentLevel == progress.WorldData.LevelMap.LevelNumber && 
                CurrentScene() == progress.WorldData.LevelMap.GameScene)
            {
                if (_isBossScene == progress.WorldData.LevelMap.IsBossScene)
                {
                    // boss
                    Debug.Log("Boss must be here");
                }
                else
                {
                    // instantiate dungeon
                    var savedMap = progress.WorldData.LevelMap.Dungeon;
                    foreach (var tile in savedMap)
                    {
                        _currentMap.Add(tile.AsUnityVector2());
                    }
                    GameActions.Current.LevelLoaded(_currentMap);
                }
            }

           
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            HashSet<Vector2Data> tilesToSave = new HashSet<Vector2Data>();
            foreach (var tile in _currentMap)
            {
                tilesToSave.Add(tile.AsTileData());
            }

            progress.WorldData.LevelMap = new LevelMap(_currentWorld, _currentLevel, CurrentScene(), _isBossScene, tilesToSave);
        }

        private static string CurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}