using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Dungeon
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] randomItems;
        [SerializeField] private GameObject[] randomEnemies;
        [SerializeField] private GameObject[] randomTraps;

        [SerializeField] private DungeonType dungeonType;
        
        [SerializeField] protected internal GameObject wallPrefab;
        [SerializeField] private GameObject roomPrefab;
        [SerializeField] protected internal GameObject mainFloorTile;


        [SerializeField] protected internal Sprite[] floorTiles;
        
        [SerializeField] private bool useComplexWallTiles;
        [SerializeField] private GameObject[] wallTiles;

        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject exitDoorPrefab;
        
        [SerializeField] private LayerMask whatIsFloor;
        [SerializeField] private LayerMask whatIsWall;
        [SerializeField] private Tag tileSpawnerTag;
        
        [Range(10, 5000)]
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
 
        [SerializeField] private Vector2 maxRoomSize = new Vector2(7,7);
        
        [Header("Boss")]
        [SerializeField] private bool finalDungeonInWorld;
        [SerializeField] private int corridorLengthToBoss = 20;
        [SerializeField] private Vector2 bossRoomSize  = new Vector2(9,9);
        [SerializeField] private GameObject boss;
     

        protected internal float MinX, MaxX, MinY, MaxY;
        protected internal List<GameObject> _walls = new List<GameObject>();

        private List<Vector3> _floorList = new List<Vector3>();
        //private List<Vector3> _roomCentresList = new List<Vector3>();
        private Vector2 _hitSize;
       

        private void Start()
        {
            _hitSize = Vector2.one * 0.8f;
          //  GenerateField();
            switch (dungeonType)
            {
                case DungeonType.Caverns:
                    StartRandomWalker();
                    break;
                case DungeonType.Rooms:
                    StartRoomWalker();
                    break;
                case DungeonType.Winding:
                    StartWindingWalker();
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
            while (_floorList.Count < totalFloorCount )
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
            while (_floorList.Count < totalFloorCount )
            {
                int walkLength = Random.Range(minHallLength, maxHallLength);
                currentPos = GenerateLongWalk(currentPos, walkLength);
                GenerateRoom(currentPos, maxRoomSize, true);
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
                int walkLength = Random.Range(minHallLength, maxHallLength);
                currentPos = GenerateLongWalk(currentPos, walkLength);
                int roll = Random.Range(0, 100);
                if (roll > hallsFrequency)
                {
                    GenerateRoom(currentPos, maxRoomSize, true);
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

        private Vector3 GenerateLongWalk(Vector3 pos, int walkLength)
        {
            // creating halls
            Vector3 walkDirection = GetRandomDirection();
           
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
        private void GenerateRoom(Vector3 pos, Vector2 maxSize, bool isRandom)
        {
            int roomWidth = 0;
            int roomHeight = 0;
            if (isRandom)
            {
                // create a random room at the end of walk length
                roomWidth = Random.Range(1, Mathf.CeilToInt(maxSize.x/2f)); // 4 tiles up and 4 tiles down relating to hallway (1) = 4 + 4 + 1
                roomHeight = Random.Range(1, Mathf.CeilToInt(maxSize.y/2f));
            }
            else
            {
                // create a random room at the end of walk length
                roomWidth = Mathf.CeilToInt(maxSize.x/2f); // 4 tiles up and 4 tiles down relating to hallway (1) = 4 + 4 + 1
                roomHeight = Mathf.CeilToInt(maxSize.y/2f);
            }
           
            Vector3 center = new Vector3(Mathf.Floor(roomWidth/2), Mathf.Floor(roomHeight/2), 0);
            //bool roomReady = false;
  
            for (int w = -roomWidth; w <= roomWidth; w++)
            {
                for (int h = -roomHeight; h <= roomHeight; h++)
                {
                    Vector3 offset = new Vector3(w, h, 0);
                    //Vector3 center = new Vector3(Mathf.Floor(w/2), Mathf.Floor(h/2), 0);
                    
                    if (!CheckIfInFloorList(pos + offset)) 
                    {
                        _floorList.Add(pos + offset);
                        
                        //newRoom.AddTileToRoom(pos + offset);   
                        /*if (!CheckIfInRoomCentreList(pos + center) && !roomReady)
                        {
                            _roomCentresList.Add(pos + center);
                          
                            roomReady = true;
                        }
                        */
                    }
                }
            }
        }
        private bool CheckIfInFloorList(Vector3 tilePos)
        {
            //bool inFloorList = false;
            if (_floorList.Contains(tilePos)) return true;
            return false;
        }

        // should work after base dungeon is generated
        private IEnumerator DelayedProgress()
        { 

            GenerateBossPath();
            GenerateFloorTiles();
                
            if (gameObject.FindAllWithTag(tileSpawnerTag).Count > 0)
            {
                yield return null;
            }

            //yield return StartCoroutine(InsertPlaceForExitDoor());
            
            SetExitDoor();
            CheckGeneratedDungeon();
            

            // Recalculate only the first grid graph
            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
        }

        private void GenerateFloorTiles()
        {
            // generating tiles (they change to floor)
            for (int i = 0; i < _floorList.Count; i++)
            {
                //Vector2Int targetTilePos = new Vector2Int((int)_floorList[i].x, (int)_floorList[i].y);
                //floorTilemap.SetTile((Vector3Int)targetTilePos, floorTile);
                
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

        private void GenerateBossPath()
        {
            Vector2 direction = new Vector2(0, 0);
            Vector2 currentPos = _floorList[_floorList.Count - 1];
            int path = 0;
            
            //opposite direction from start point
            Vector2 pointA = _floorList[0];
            Vector2 pointB = _floorList[_floorList.Count - 1];
            float xDist = Mathf.Abs(pointA.x - pointB.x);
            float yDist = Mathf.Abs(pointA.y - pointB.y);
            if(xDist > yDist) {
                direction.x = pointB.x >= pointA.x ? 1f : -1f;
            }
            else {
                direction.y = pointB.y >= pointA.y ? 1f : -1f;
            }
         
            
            while (path < corridorLengthToBoss)
            {
                currentPos += direction;
                if (!CheckIfInFloorList(currentPos)) 
                {
                    _floorList.Add(currentPos);
                    path++;
                }
                else
                {
                    path = 0;
                }
            }
          
            if (path >= corridorLengthToBoss)
            {
                Debug.Log(CheckAvailableSpaceForBossRoom(currentPos, bossRoomSize));
                GenerateRoom(currentPos, bossRoomSize, false);
                
                
               /* if (CheckAvailableSpaceForBossRoom(currentPos, bossRoomSize))
                {
                    GenerateRoom(currentPos, bossRoomSize, false);
                }
                else
                {
                    Debug.Log("Recursive");
                    GenerateBossPath();
                }*/
                
              
            }
        }

        private bool CheckAvailableSpaceForBossRoom(Vector3 pos, Vector2 roomSize)
        {
            int roomWidth = Mathf.CeilToInt((roomSize.x + 2) / 2f); 
            int roomHeight = Mathf.CeilToInt((roomSize.y + 2) / 2f);
            Debug.Log(roomWidth);

            for (int w = -roomWidth; w <= roomWidth; w++)
            {
                for (int h = -roomHeight; h <= roomHeight; h++)
                {
                    Vector3 offset = new Vector3(w, h, 0);

                    if (CheckIfInFloorList(pos + offset))
                    {
                       
                        return false;
                      
                    }
                }
            }

            return true;
        }

        private IEnumerator InsertPlaceForExitDoor()
        {
            Vector3 dir = GetRandomDirection();
            bool isExitPlaced = IfExitIsPlaced(_floorList[_floorList.Count - 1]); // last tile
            while (!isExitPlaced)
            {
                Vector3 currentPos = WalkStraightInDir(_floorList[_floorList.Count - 1], dir);
              
                yield return null;
                isExitPlaced = IfExitIsPlaced(currentPos);
            }

            yield return null;
        }

        private Vector3 WalkStraightInDir(Vector3 pos, Vector3 dir)
        {
            Vector3 currentPos = _floorList[_floorList.Count - 1] + dir;

                if (CheckIfInFloorList(currentPos))
                {
                    _floorList.Remove(currentPos);
                }
                else
                {
                    Collider2D hitWall = Physics2D.OverlapBox(currentPos, _hitSize, 0, whatIsWall);
                    if (hitWall)
                    {
                        DestroyImmediate(hitWall.gameObject);
                    }

                    GameObject tileGO = Instantiate(tilePrefab, currentPos, Quaternion.identity, transform);
                    tileGO.name = tilePrefab.name;

                }
                _floorList.Add(currentPos);
                return currentPos;
                
        }
        
        private bool IfExitIsPlaced(Vector2 tilePos)
        {
            int numWalls = 0; // we need 3 walls to surround exit door;
            if (Physics2D.OverlapBox(tilePos + Vector2.up, _hitSize, 0, whatIsWall)) { numWalls++; }
            if (Physics2D.OverlapBox(tilePos + Vector2.right, _hitSize, 0, whatIsWall)) { numWalls++; }
            if (Physics2D.OverlapBox(tilePos + Vector2.down, _hitSize, 0, whatIsWall)) { numWalls++; }
            if (Physics2D.OverlapBox(tilePos + Vector2.left, _hitSize, 0, whatIsWall)) { numWalls++; }

            if (numWalls == 3)
            {
                SetExitDoor();
                return true;
            }
            return false;
        }
       
        private void CheckGeneratedDungeon ()
        {
            for (int x = (int)MinX - 2; x< (int)MaxX + 2; x++)
            {
                for (int y = (int)MinY - 2; y< (int)MaxY + 2; y++)
                {
                    if (useComplexWallTiles || useRoundedEdges)
                    {
                        SetWallTiles(x, y);
                    }
                   
                    
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
                }
            }
        }

        public void SetWallTiles(int x, int y)
        {
            Collider2D hitWall = Physics2D.OverlapBox(new Vector2(x, y), _hitSize, 0, whatIsWall);
            
            // checking if there is any wall tiles around current tile
            Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), _hitSize , 0, whatIsWall);
            Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), _hitSize , 0, whatIsWall);
            Collider2D hitBottom = Physics2D.OverlapBox(new Vector2(x, y - 1), _hitSize , 0, whatIsWall);
            Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), _hitSize, 0, whatIsWall);
                    
            Collider2D hitTopFloor = Physics2D.OverlapBox(new Vector2(x, y + 1), _hitSize , 0, whatIsFloor);
            Collider2D hitRightFloor = Physics2D.OverlapBox(new Vector2(x + 1, y), _hitSize , 0, whatIsFloor);
            Collider2D hitBottomFloor = Physics2D.OverlapBox(new Vector2(x, y - 1), _hitSize , 0, whatIsFloor);
            Collider2D hitLeftFloor = Physics2D.OverlapBox(new Vector2(x - 1, y), _hitSize, 0, whatIsFloor);

            // assigning bit value if there is any wall tiles around current one
            int bitValue = 0;
            
            if (hitWall)
                {
                    if (!hitTop && hitTopFloor) { bitValue += 1; }
                    if (!hitRight && hitRightFloor) { bitValue += 2; }
                    if (!hitBottom && hitBottomFloor) { bitValue += 4; }
                    if (!hitLeft && hitLeftFloor) { bitValue += 8; }

                    //if (bitValue > 0)
                   // {
                        // setting correct edge
                        GameObject wallGo =
                            Instantiate(wallTiles[bitValue], new Vector2(x, y), Quaternion.identity);
                        wallGo.name = wallTiles[bitValue].name;
                        wallGo.transform.SetParent(hitWall.transform);
                   // }

                   
                    //Debug.Log(bitValue);
                }
            
            if (useRoundedEdges)
            {
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
        
       
    }
}