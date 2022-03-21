using System;
using System.Collections.Generic;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Infrastructure.Services.SaveLoad;
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
        
        public List<Vector2> CurrentMap { get; private set; } = new List<Vector2>();

   

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
            CurrentMap = mapTiles;
        }
        public void LoadProgress(PlayerProgress progress)
        {
            if (_currentWorld == progress.WorldData.LevelMap.WorldNumber &&
                _currentLevel == progress.WorldData.LevelMap.LevelNumber && 
                CurrentScene() == progress.WorldData.LevelMap.GameScene)
            {
                if (_isBossScene == progress.WorldData.LevelMap.IsBossScene && _isBossScene)
                {
                    // boss
                    Debug.Log("Boss must be here");
                }
                else
                {
                    // instantiate dungeon
                    List<Vector2Data> savedMap = progress.WorldData.LevelMap.Dungeon;
                    if (savedMap != null)
                    {
                        foreach (Vector2Data tile in savedMap)
                        {
                            CurrentMap.Add(tile.AsUnityVector2());
                        }
                        GameActions.Current.LevelLoaded(CurrentMap);
                    }

                }
            }

           
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            List<Vector2Data> tilesToSave = new List<Vector2Data>();
            foreach (Vector2 tile in CurrentMap)
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