using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Dungeon
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] randomItems;
        [SerializeField] private GameObject[] randomEnemies;
        [SerializeField] private GameObject boss;
        
        [SerializeField] private DungeonType dungeonType;
        
        [SerializeField] protected internal GameObject wallPrefab;
        [SerializeField] protected internal GameObject floorPrefab;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject exitDoorPrefab;
        
        [SerializeField] private LayerMask whatIsFloor;
        [SerializeField] private LayerMask whatIsWall;
        [SerializeField] private Tag tileSpawnerTag;
        
        [Range(50, 5000)]
        [SerializeField] private int totalFloorCount;
        [Range(0, 100)]
        [SerializeField] private int itemSpawnPercent;

        [SerializeField] private bool useRoundedEdges;
        [SerializeField] private GameObject[] roundedEdges;

        [Header("For Dungeon with ROOMS")] 
        [Range(1,100)]
        [SerializeField] private int hallsFrequency = 50;
        [Range(1, 20)]
        [SerializeField] private int minHallLength = 5;
        [Range(1, 20)]
        [SerializeField] private int maxHallLength = 20;
        [Range(3, 9)]
        [SerializeField] private int maxRoomSize = 9;

        protected internal float MinX, MaxX, MinY, MaxY;

        private List<Vector3> _floorList = new List<Vector3>();
        private List<Vector3> _roomCentresList = new List<Vector3>();
        private Vector2 _hitSize;

        private void Start()
        {
            _hitSize = Vector2.one * 0.8f;
            
            // initiating dungeon generation
            switch (dungeonType)
            {
                case DungeonType.Caverns: StartRandomWalker();
                    break;
                case DungeonType.Rooms: StartRoomWalker();
                    break;
                case DungeonType.Winding: StartWindingWalker();
                    break;
            }
            
        }

        private void Update()
        {
            // for testing
            if (Application.isEditor && Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        // for cavern type dungeon
        private void StartRandomWalker()
        {
            Vector3 currentPos = Vector3.zero;
            _floorList.Add(currentPos);
            //set floor tile at current position
            while (_floorList.Count < totalFloorCount)
            {
                currentPos += GetRandomDirection();

                if (!CheckIfInFloorList(currentPos)) 
                {
                    _floorList.Add(currentPos);
                }
            }
            StartCoroutine(DelayedProgress());
        }

        // for room type dungeon
        private void StartRoomWalker()
        {
            
            Vector3 currentPos = Vector3.zero;
            _floorList.Add(currentPos);
            //set floor tile at current position
            while (_floorList.Count < totalFloorCount)
            {
                currentPos = GenerateLongWalk(currentPos);
                GenerateRandomRoom(currentPos);
            }
            StartCoroutine(DelayedProgress());
 
        }

        private void StartWindingWalker()
        {
            Vector3 currentPos = Vector3.zero;
            _floorList.Add(currentPos);
            //set floor tile at current position
            while (_floorList.Count < totalFloorCount)
            {
                currentPos = GenerateLongWalk(currentPos);
                int roll = Random.Range(0, 100);
                if (roll > hallsFrequency)
                {
                    GenerateRandomRoom(currentPos);
                }
               
            }
            StartCoroutine(DelayedProgress());
        }
        // choose any of 4 directions
        private Vector3 GetRandomDirection()
        {
            switch (Random.Range(1, 5))
            {
                case 1: return Vector3.up;
                case 2: return Vector3.right;
                case 3: return Vector3.down;
                case 4: return Vector3.left;
            }

            return Vector3.zero;
        }

        private Vector3 GenerateLongWalk(Vector3 pos)
        {
            // creating halls
            Vector3 walkDirection = GetRandomDirection();
            int walkLength = Random.Range(minHallLength, maxHallLength);
            for (int i = 0; i < walkLength; i++)
            {
                if (!CheckIfInFloorList(pos + walkDirection)) 
                {
                    _floorList.Add(pos + walkDirection);
                }

                pos += walkDirection;
            }

            return pos;
        }
        private void GenerateRandomRoom(Vector3 pos)
        {
            // create a random room at the end of walk length
            int roomWidth = Random.Range(1, Mathf.CeilToInt(maxRoomSize/2f)); // 4 tiles up and 4 tiles down relating to hallway (1) = 4 + 4 + 1
            int roomHeight = Random.Range(1, Mathf.CeilToInt(maxRoomSize/2f));
               
            bool roomReady = false; 
              
            for (int w = -roomWidth; w < roomWidth; w++)
            {
                for (int h = -roomHeight; h < roomHeight; h++)
                {
                    Vector3 offset = new Vector3(w, h, 0);
                    Vector3 center = new Vector3(Mathf.Floor(w/2), Mathf.Floor(h/2), 0);
                    if (!CheckIfInFloorList(pos + offset)) 
                    {
                        _floorList.Add(pos + offset);
                           
                        if (!CheckIfInRoomCentreList(pos + center) && !roomReady)
                        {
                            _roomCentresList.Add(pos + center);
                          
                            roomReady = true;
                        }
                    }
                }
            }
        }
        private bool CheckIfInFloorList(Vector3 tilePos)
        {
            //bool inFloorList = false;
            for (int i = 0; i < _floorList.Count; i++)
            {
                if (Vector3.Equals(tilePos, _floorList[i]))
                {
                    //inFloorList = true;
                    return true;
                }
            }
            return false;
        }
        private bool CheckIfInRoomCentreList(Vector3 pos)
        {
    
            for (int i = 0; i < _roomCentresList.Count; i++)
            {
                if (Vector3.Equals(pos, _roomCentresList[i]))
                {

                    return true;
                }
            }
            return false;
        }
        // should work after base dungeon is generated
        private IEnumerator DelayedProgress()
        {
            GenerateFloorTiles();
                
            if (gameObject.FindAllWithTag(tileSpawnerTag).Count > 0)
            {
                yield return null;
            }

            //GenerateBossRoom();
            //SetBoss();
            SetExitDoor();
            CheckGeneratedDungeon();
        }

        private void GenerateFloorTiles()
        {
            // generating tiles (they change to floor)
            for (int i = 0; i < _floorList.Count; i++)
            {
                GameObject tileGo = Instantiate(tilePrefab, _floorList[i], Quaternion.identity);
                tileGo.name = tilePrefab.name;
                tileGo.transform.SetParent(transform);
            }
        }
        private void SetExitDoor()
        {
            Vector3 doorPos = _floorList[_floorList.Count - 1];
            GameObject doorGo = Instantiate(exitDoorPrefab, doorPos, Quaternion.identity);
            doorGo.name = exitDoorPrefab.name;
            doorGo.transform.SetParent(transform);
        }

        private void GenerateBossRoom()
        {
            Vector3 lastPos = _floorList[_floorList.Count - 1];
            // boss room
            int bossRoomWidth = 4;
            int bossRoomHeight = 4;
            bool roomReady = false;
            for (int w = -bossRoomWidth; w < bossRoomWidth; w++)
            {
                for (int h = -bossRoomHeight; h < bossRoomHeight; h++)
                {
                    Vector3 offset = new Vector3(w, h, 0);
                    Vector3 center = new Vector3(Mathf.Floor(w/2), Mathf.Floor(h/2), 0);
                       
                    if (!CheckIfInFloorList(lastPos + offset)) 
                    {
                        _floorList.Add(lastPos + offset);
                        if (!CheckIfInRoomCentreList(lastPos + center) && !roomReady)
                        {
                            _roomCentresList.Add(lastPos + center);
                          
                            roomReady = true;
                        }
                          
                    }
                }
            }
        }
        private void SetBoss()
        {
            Vector3 bossPos = _roomCentresList[_roomCentresList.Count - 1];
            GameObject bossGo = Instantiate(boss, bossPos, Quaternion.identity);
            bossGo.name = exitDoorPrefab.name;
            bossGo.transform.SetParent(transform);
        }

        private void CheckGeneratedDungeon ()
        {
            for (int x = (int)MinX - 2; x< (int)MaxX + 2; x++)
            {
                for (int y = (int)MinY - 2; y< (int)MaxY + 2; y++)
                {
                    Collider2D hitFloor = Physics2D.OverlapBox(new Vector2(x, y), _hitSize , 0, whatIsFloor);
                    if (hitFloor)
                    {
                        // if not last generated tile
                        if (!Vector2.Equals(hitFloor.transform.position, _floorList[_floorList.Count - 1]))
                        {
                          
                            Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), _hitSize , 0, whatIsWall);
                            Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), _hitSize , 0, whatIsWall);
                            Collider2D hitBottom = Physics2D.OverlapBox(new Vector2(x, y - 1), _hitSize , 0, whatIsWall);
                            Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), _hitSize, 0, whatIsWall);

                            SetRandomItems( hitFloor, hitTop,  hitRight,  hitBottom, hitLeft);
                        }
                    }
                    GenerateRoundedEdgesForWalls(x, y);
                }
            }
        }

        private void SetRandomItems( Collider2D hitFloor, Collider2D hitTop,  Collider2D hitRight,  Collider2D hitBottom,  Collider2D hitLeft)
        {
            if ((hitTop || hitRight || hitBottom || hitLeft)
                && !(hitTop && hitBottom)
                && !(hitRight && hitLeft)){
                            
                int roll = Random.Range(1, 101);
                if (roll <= itemSpawnPercent)
                {
                    int itemIndex = Random.Range(0, randomItems.Length);
                    GameObject itemGo = Instantiate(randomItems[itemIndex], hitFloor.transform.position,
                        Quaternion.identity);
                    itemGo.name = randomItems[itemIndex].name;
                    itemGo.transform.SetParent(hitFloor.transform);
                }
            }
        }
        
        private void GenerateRoundedEdgesForWalls(int x, int y)
        {
            if (useRoundedEdges)
            {
                Collider2D hitWall = Physics2D.OverlapBox(new Vector2(x, y), _hitSize, 0, whatIsWall);
                if (hitWall)
                {
                    // checking if there is any wall tiles around current tile
                    Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), _hitSize , 0, whatIsWall);
                    Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), _hitSize , 0, whatIsWall);
                    Collider2D hitBottom = Physics2D.OverlapBox(new Vector2(x, y - 1), _hitSize , 0, whatIsWall);
                    Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), _hitSize, 0, whatIsWall);

                    // assigning bit value if there is any wall tiles around current one
                    int bitValue = 0;
                    if (!hitTop) { bitValue += 1; }
                    if (!hitRight) { bitValue += 2; }
                    if (!hitBottom) { bitValue += 4; }
                    if (!hitLeft) { bitValue += 8; }

                    if (bitValue > 0)
                    {
                        // setting correct edge
                        GameObject wallEdgeGo =
                            Instantiate(roundedEdges[bitValue], new Vector2(x, y), Quaternion.identity);
                        wallEdgeGo.name = roundedEdges[bitValue].name;
                        wallEdgeGo.transform.SetParent(hitWall.transform);
                    }
                }
            }
        }
    }
}