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

        //public bool ShouldRegenerateDungeon;
       // public Vector3Data PlayerPosition;
        //public List<Vector2Data> Dungeon;

        public LevelMap(int worldNumber, int levelNumber, string gameScene,bool isBossScene)
        {
            WorldNumber = worldNumber;
            LevelNumber = levelNumber;
            GameScene = gameScene;
            IsBossScene = isBossScene;
            //ShouldRegenerateDungeon = shouldRegenerateDungeon;
            //Dungeon = dungeon;
        }
  
    }
}