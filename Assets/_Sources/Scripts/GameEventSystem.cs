using System;
using UnityEngine;

namespace _Sources.Scripts
{
    public class GameEventSystem : MonoBehaviour
    {
        public static GameEventSystem current;

        private void Awake()
        {
            current = this;
        }

        public event Action OnDungeonGenerated;

        public void DungeonGenerated()
        {
            if (OnDungeonGenerated != null)
            {
                OnDungeonGenerated();
            }
        }
    }
}