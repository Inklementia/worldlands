using System;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure
{
    public class GameBootStrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            DontDestroyOnLoad(this);
        }
    }
}
