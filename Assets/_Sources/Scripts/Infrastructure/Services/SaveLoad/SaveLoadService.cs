using System;
using System.Collections.Generic;
using System.IO;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Factory;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService persistentProgressService, IGameFactory gameFactory)
        {
            _persistentProgressService = persistentProgressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_persistentProgressService.PlayerProgress);
            }

            string saveString = _persistentProgressService.PlayerProgress.ToJson();
            PlayerPrefs.SetString(ProgressKey, saveString);
            //File.WriteAllText(Application.dataPath + "/player_progress.txt", saveString);
            ES3DataManager.Instance.SaveLevelNumber(_persistentProgressService.PlayerProgress.WorldData.LevelMap.LevelNumber);

        }

        public PlayerProgress LoadProgress()
        {
            // Change to EASY SAVE 3 
            /*
            if (File.Exists(Application.dataPath + "/player_progress.txt"))
            {
                string saveString = File.ReadAllText(Application.dataPath + "/player_progress.txt");
                return saveString.ToDeserialized<PlayerProgress>();
            }*/
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
            //return null;

        }

   
    }
}