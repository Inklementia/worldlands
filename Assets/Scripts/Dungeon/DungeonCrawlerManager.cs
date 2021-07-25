using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Up,
    Left,
    Down,
    Right
}
public class DungeonCrawlerManager : MonoBehaviour
{
 
    public static List<Vector2Int> PositionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> _directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.Up, Vector2Int.up},
        {Direction.Left, Vector2Int.left},
        {Direction.Down, Vector2Int.down},
        {Direction.Right, Vector2Int.right}
    };


    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        for (int i = 0; i < dungeonData.NumberofCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
           
        }

        int iterations = Random.Range(dungeonData.IterationMin, dungeonData.IterationMax);
        

        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(_directionMovementMap);
                PositionsVisited.Add(newPos);
               
            }
        }
 
        return PositionsVisited;
    }
}
