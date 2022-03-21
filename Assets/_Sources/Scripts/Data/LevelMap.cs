using System;
using System.Collections.Generic;

namespace _Sources.Scripts.Data
{
    [Serializable]
    public class LevelMap
    {
        public int WorldNumber;
        public int LevelNumber;
        
        public string GameScene; // boss or world

        public bool IsBossScene;
       // public Vector3Data PlayerPosition;
        public List<Vector2Data> Dungeon;

        public LevelMap(int worldNumber, int levelNumber, string gameScene, bool isBossScene, List<Vector2Data> dungeon)
        {
            WorldNumber = worldNumber;
            LevelNumber = levelNumber;
            GameScene = gameScene;
            IsBossScene = isBossScene;
            Dungeon = dungeon;
        }
  
    }
}