using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Factory;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private IPersistentProgressService _persistentProgressService;
        private IGameFactory _gameFactory;

        public SaveLoadService()
        {
         
        }

        public void SaveProgress()
        {
            
        }

        public PlayerProgress LoadProgress()
        {
            // Change to EASY SAVE 3 
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}