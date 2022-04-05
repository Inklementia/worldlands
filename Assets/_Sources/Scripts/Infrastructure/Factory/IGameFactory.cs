using System.Collections.Generic;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject playerInitialPoint);
        void CreateHud();
        GameObject CreateWorldManager();
        GameObject CreateDungeon();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        void DeleteAllInstanciatedObjects();
    }
}