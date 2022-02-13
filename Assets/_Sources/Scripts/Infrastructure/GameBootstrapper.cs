using _Sources.Scripts.Infrastructure.GameStates;
using _Sources.Scripts.UI;
using MoreMountains.Tools;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        
    //private GameBootstrapper instance;
    [SerializeField] private LoadingScreen loadingScreen;

    private Game _game;

    private void Awake()
    {
        /*
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        StartNewGame();
        */
        _game = new Game(this, loadingScreen);
        _game.StateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
      
    }

    public void StartNewGame()
    {
        _game = new Game(this, loadingScreen);
        _game.StateMachine.Enter<BootstrapState>();
    }
    }
}
