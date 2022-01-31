using _Sources.Scripts.Services.Input;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            // smth like Input,...
            RegisterServices();
            _sceneLoader.Load(Initial, onLoaded:EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            // load main level
            _stateMachine.Enter<LoadLevelState>();
        }

        private void RegisterServices()
        {
            Game.InputService = SetUpInputService();
        }

        public void Exit()
        {
            
        }
        
        private static IInputService SetUpInputService()
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