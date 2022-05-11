using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Data;
using _Sources.Scripts.Dungeon;
using _Sources.Scripts.Helpers;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.Input;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Infrastructure.Services.SaveLoad;
using _Sources.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace _Sources.Scripts
{
    public class WorldManager : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private WorldDataSO worldData;
        [SerializeField] private GameObject playerSpawnPoint;

        public int CurrentLevel { get; private set; }
        private int _currentWorld = 1;
        private bool _isBossScene = false;
        private bool _shouldRegenerate;
        private ISaveLoadService _saveLoadService;
        public RoomDungeonGenerator generator;
        
        private IInputService _inputService;
        private LevelTransfer _levelTransfer;

        private bool _once;
        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _levelTransfer = transform.GetComponent<LevelTransfer>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }
    
        private void Start()
        {
            //generator = GameObject.FindObjectOfType<RoomDungeonGenerator>();
 

          
            //Debug.Log("Current Level: "+CurrentLevel);
        }

        private void SetPlayerAtStartPosition(Vector3 place)
        {
            playerSpawnPoint.transform.position = place;
        }

        private void OnEnable()
        {
            //GameActions.Current.OnDungeonGeneratedToSaveMap += SaveCurrentMap;
            //GameActions.Current.OnDungeonFinished += IncreaseLevel;
        }

        private void OnDisable()
        {
            //GameActions.Current.OnDungeonGeneratedToSaveMap -= SaveCurrentMap;
            //GameActions.Current.OnDungeonFinished -= IncreaseLevel;
            
        }

        private void Update()
        {
            if (_inputService.IsRegenerateButtonPressed() && _once)
            { 
                _once = false;
                _levelTransfer.RunLevel("Main");
               
            }
        }


        public void IncreaseLevel()
        {
            //ebug.Log("lEVEL uP "+CurrentLevel);
            CurrentLevel++;
         
            //Debug.Log("lEVEL uP "+CurrentLevel);
        }

        public void ResetLevel()
        {
            CurrentLevel = 1;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            
            _once = true;
            if (CurrentScene() == progress.WorldData.LevelMap.GameScene)
            {
                if (_isBossScene == progress.WorldData.LevelMap.IsBossScene && _isBossScene)
                {
                    // boss
                    //Debug.Log("Boss must be here");
               
                }
                else
                {
                    //Debug.Log("normal level here");
                
                   
                    //GameActions.Current.RegenerateDungeon();
                    // instantiate dungeon
                    //CurrentLevel = progress.WorldData.LevelMap.LevelNumber;
                    /*
                    List<Vector2Data> savedMap = progress.WorldData.LevelMap.Dungeon;
                    if (savedMap != null)
                    {
                        foreach (Vector2Data tile in savedMap)
                        {
                            CurrentMap.Add(tile.AsUnityVector2());
                        }
                        GameActions.Current.LevelLoaded(CurrentMap);
                    }*/

                }

                CurrentLevel = progress.WorldData.LevelMap.LevelNumber;
                if (CurrentLevel == 0)
                {
                    CurrentLevel = 1;
                }
                _currentWorld = progress.WorldData.LevelMap.WorldNumber;
                Debug.Log(CurrentLevel);
                GameObject.FindWithTag("LevelNumber").GetComponent<TMP_Text>()
                   .SetText(CurrentLevel.ToString());
                if (generator != null)
                {
                    generator.GenerateDungeon();
                }
                /*
                
                if (CurrentLevel == worldData.NumberOfLevels)
                {
                    //Debug.Log("END OF WORLD");
                    CurrentLevel = 1;
                    progress.WorldData.LevelMap.LevelNumber = CurrentLevel;
                    _saveLoadService.SaveProgress();
                }*/
                
                _saveLoadService.SaveProgress();
            }

           
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            /*
            List<Vector2Data> tilesToSave = new List<Vector2Data>();
            if(CurrentMap != null)
            {
                foreach (Vector2 tile in CurrentMap)
                {
                    tilesToSave.Add(tile.AsTileData());
                }
            }*/
           
            progress.WorldData.LevelMap = new LevelMap(_currentWorld, CurrentLevel, CurrentScene(),_isBossScene);
            
        }

        private static string CurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}