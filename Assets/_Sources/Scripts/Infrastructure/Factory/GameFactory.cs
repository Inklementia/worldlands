using System.Collections.Generic;
using _Sources.Scripts.Infrastructure.AssetManagement;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private List<GameObject> _instanciatedObjects = new List<GameObject>();
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject playerSpawnPoint)
        {
           
            return InstantiateAndRegister(AssetPath.PlayerPath, playerSpawnPoint.transform.position);
        }

        public GameObject CreateHud()
        {
             return InstantiateAndRegister(AssetPath.HudPath);
        } 
     

        public GameObject CreateWorldManager()
        {
            return InstantiateAndRegister(AssetPath.WorldPath, Vector3.zero);
        }
        public GameObject CreateDungeon()
        {
            return InstantiateAndRegister(AssetPath.Dungeon, Vector3.zero);
        }
        private GameObject InstantiateAndRegister(string prefabPath, Vector3 position)
        {
           
            GameObject gameObject = _assets.Instantiate(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            _instanciatedObjects.Add(gameObject);
            return gameObject;
        }
        private GameObject InstantiateAndRegister(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            _instanciatedObjects.Add(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in
                     gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }


        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void DeleteAllInstanciatedObjects()
        {
            if (_instanciatedObjects.Count > 0)
            {
                foreach (GameObject insObject in _instanciatedObjects)
                {
                    Object.Destroy(insObject);
                }
                _instanciatedObjects.Clear();
            }
           
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            ProgressReaders.Add(progressReader);
        }
    }
}