using _Sources.Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Sources.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject playerInitialPoint);
        void CreateHud();
    }
}