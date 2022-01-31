using System.IO;
using _Sources.Scripts.UI;
using Cinemachine;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string SpawnPoint = "SpawnPoint";
        private const string PlayerCamera = "PlayerCamera";
        private const string HudPath = "UI/HUD";
        private const string PlayerPath = "Player/Player";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
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
            Instantiate(HudPath);

            //load player
            var playerSpawnPoint = GameObject.FindWithTag(SpawnPoint);
            GameObject player = Instantiate(PlayerPath, playerSpawnPoint.transform.position);

            //assign cinemachine camera to player
            GameObject.FindWithTag(PlayerCamera).GetComponent<CinemachineVirtualCamera>().Follow = player.transform;

            //when everything is loaded - enter Game loop state
            _stateMachine.Enter<GameLoopState>();
            
        }


        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);

        }
        private static GameObject Instantiate(string path, Vector3 place)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, place, Quaternion.identity);
        }

    }
}