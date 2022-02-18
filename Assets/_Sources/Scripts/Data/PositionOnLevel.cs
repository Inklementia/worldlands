namespace _Sources.Scripts.Data
{
    public class PositionOnLevel
    {
        public int WorldNumber;
        public int LevelNumber;
        public Vector3Data PlayerPosition;
        public TileData[] Dungeon;

        public PositionOnLevel(int worldNumber, int levelNumber, Vector3Data playerPosition, TileData[] dungeon)
        {
            WorldNumber = worldNumber;
            LevelNumber = levelNumber;
            PlayerPosition = playerPosition;
            Dungeon = dungeon;
        }
    }
}