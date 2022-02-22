using _Sources.Scripts.Infrastructure.Factory;
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
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            //load heads-up-display
            _gameFactory.CreateHud();
Debug.Log("FIRSt");
            //load player
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(SpawnPoint));
          

            //assign cinemachine camera to player
            GameObject.FindWithTag(PlayerCamera).GetComponent<CinemachineVirtualCamera>().Follow = player.transform;

            //when everything is loaded - enter Game loop state
            _stateMachine.Enter<GameLoopState>();
            
        }
    }
}