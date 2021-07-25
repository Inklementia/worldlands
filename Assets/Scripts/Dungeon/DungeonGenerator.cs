using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> _dungeonRooms;

    //public int GeneratedRoomsCount;

    private void Start()
    {
        _dungeonRooms = DungeonCrawlerManager.GenerateDungeon(dungeonGenerationData);
        //GeneratedRoomsCount = _dungeonRooms.Distinct().Count();
        SpawnRooms(_dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {

        RoomManager.instance.LoadRoom("Start", 0, 0);
    
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomManager.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
        }
    }
}
