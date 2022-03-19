using UnityEngine;

namespace _Sources.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        
        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject playerSpawnPoint) => 
            _assets.Instantiate(AssetPath.PlayerPath, playerSpawnPoint.transform.position);
        public void CreateHud() => 
            _assets.Instantiate(AssetPath.HudPath);

        public GameObject CreateWorldManager() => 
            _assets.Instantiate(AssetPath.WorldPath, Vector3.zero);
    }
}