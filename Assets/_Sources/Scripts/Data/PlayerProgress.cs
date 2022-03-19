using System;

namespace _Sources.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string initialLevelScene)
        {
            WorldData = new WorldData(initialLevelScene);
        }
    }
}