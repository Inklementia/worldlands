using _Sources.Scripts.UI;
using MoreMountains.Tools;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen loadingScreen;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, loadingScreen);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}
