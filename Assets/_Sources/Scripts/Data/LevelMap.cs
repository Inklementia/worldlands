using System.Collections.Generic;

namespace _Sources.Scripts.Data
{
    public class LevelMap
    {
        public int WorldNumber;
        public int LevelNumber;
        
        public string GameScene; // boss or world

        public bool IsBossScene;
       // public Vector3Data PlayerPosition;
        public HashSet<Vector2Data> Dungeon;

        public LevelMap(int worldNumber, int levelNumber, string gameScene, bool isBossScene, HashSet<Vector2Data> dungeon)
        {
            WorldNumber = worldNumber;
            LevelNumber = levelNumber;
            GameScene = gameScene;
            isBossScene = IsBossScene;
            //PlayerPosition = playerPosition;
            Dungeon = dungeon;
        }
  
    }
}