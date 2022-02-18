using _Sources.Scripts.Infrastructure.Factory;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.Input;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _allServices;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _allServices = allServices;
            
            RegisterServices(); //registering links/references for services
        }
        
        public void Enter()
        {
            //registering services were here
            _sceneLoader.Load(Initial, onLoaded:EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            // load main level
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }

        private void RegisterServices()
        {
            //registering services

            _allServices.RegisterSingle<IInputService>(InputService());
            _allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
            _allServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(_allServices.Single<IAssetProvider>()));
        }

        public void Exit()
        {
            
        }
        
        private static IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }
    }
}