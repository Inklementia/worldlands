using _Sources.Scripts.Services.Input;
using _Sources.Scripts.UI;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen);
        }


    }
}
