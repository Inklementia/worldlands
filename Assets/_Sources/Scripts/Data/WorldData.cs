using System;

namespace _Sources.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public LevelMap LevelMap;

        public WorldData(string initialLevelScene)
        {
            LevelMap = new LevelMap(1,1, initialLevelScene, false, null);
        }
    }
}