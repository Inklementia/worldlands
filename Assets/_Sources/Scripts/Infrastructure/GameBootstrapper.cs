using _Sources.Scripts.Infrastructure.GameStates;
using _Sources.Scripts.UI;
using MoreMountains.Tools;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        
    //private GameBootstrapper instance;
    public LoadingScreen LoadingScreenPrefab;

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
        _game = new Game(this, Instantiate(LoadingScreenPrefab));
        _game.StateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
      
    }
    }
}
