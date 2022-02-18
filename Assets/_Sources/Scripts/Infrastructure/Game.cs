using _Sources.Scripts.Infrastructure.GameStates;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.UI;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class Game
    {
     
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, AllServices.Container);
        }


    }
}
