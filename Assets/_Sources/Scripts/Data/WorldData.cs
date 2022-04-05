using System;

namespace _Sources.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public LevelMap LevelMap;

        public WorldData(string initialLevelScene , bool isBossScene)
        {
            LevelMap = new LevelMap(1,1, initialLevelScene, isBossScene);
        }
    }
}