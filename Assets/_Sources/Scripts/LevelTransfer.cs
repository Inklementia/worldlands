using System;
using _Sources.Scripts.Infrastructure;
using _Sources.Scripts.Infrastructure.GameStates;
using _Sources.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Sources.Scripts
{
    public class LevelTransfer : MonoBehaviour
    {
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        public void RunLevel(string sceneName)
        {
            //_stateMachine.Enter<LoadLevelState, string>(sceneName);
         
            Destroy( GameObject.FindObjectOfType<GameBootstrapper>());
            SceneManager.LoadScene("Initial");
        }
    }
}