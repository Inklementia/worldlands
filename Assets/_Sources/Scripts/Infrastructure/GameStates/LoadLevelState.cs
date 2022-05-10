using System;
using _Sources.Scripts.Dungeon;
using _Sources.Scripts.Infrastructure.Factory;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using _Sources.Scripts.UI;
using Cinemachine;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string SpawnPoint = "SpawnPoint";
        private const string PlayerCamera = "PlayerCamera";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        public LoadLevelState(
            GameStateMachine stateMachine, 
            SceneLoader sceneLoader, 
            LoadingScreen loadingScreen, 
            IGameFactory gameFactory,
            IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _persistentProgressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _gameFactory.CleanUp();
            //_gameFactory.DeleteAllInstanciatedObjects();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            //when everything is loaded - enter Game loop state
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
               progressReader.LoadProgress(_persistentProgressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
          
            GameObject wm = _gameFactory.CreateWorldManager();
           
            //load heads-up-display
            GameObject hud = _gameFactory.CreateHud();
            
            GameObject attackButton = GameObject.FindWithTag("AttackButton");
            GameObject pickupButton = GameObject.FindWithTag("PickupButton");
            PlayerUI playerUI = GameObject.FindWithTag("PlayerUI").GetComponent<PlayerUI>();
            //load player
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(SpawnPoint));
            player.GetComponent<PlayerInputHandler>()._attackButtonGo = attackButton;
            player.GetComponent<PlayerInputHandler>()._pickUpButtonGo = pickupButton;
            player.GetComponent<PlayerEntity>().playerUI = playerUI;
            //hud.GetComponentInChildren<PlayerUI>().player = player.GetComponent<PlayerEntity>();
                
            GameObject roomGenerator = _gameFactory.CreateDungeon();
            
            roomGenerator.GetComponentInChildren<AbstractDungeonGenerator>().spawnerManager = GameObject
                .FindWithTag("EnemySpawnerManager").GetComponent<DungeonEnemySpawnerManager>();
            wm.GetComponent<WorldManager>().generator = roomGenerator.GetComponentInChildren<RoomDungeonGenerator>();

            //assign cinemachine camera to player
            GameObject.FindWithTag(PlayerCamera).GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        }
    }
}