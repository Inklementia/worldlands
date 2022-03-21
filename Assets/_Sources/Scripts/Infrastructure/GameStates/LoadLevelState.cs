using System;
using _Sources.Scripts.Infrastructure.Factory;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
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
            _gameFactory.CreateWorldManager();
            //load heads-up-display
            _gameFactory.CreateHud();

            //load player
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(SpawnPoint));


            //assign cinemachine camera to player
            GameObject.FindWithTag(PlayerCamera).GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        }
    }
}