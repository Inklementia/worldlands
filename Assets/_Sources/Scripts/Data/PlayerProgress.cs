using System;

namespace _Sources.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string initialLevelScene, bool isBossScene)
        {
            WorldData = new WorldData(initialLevelScene, isBossScene);
        }
    }
}