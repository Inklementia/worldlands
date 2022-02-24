using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] randomEnemies;
        [Range(0, 100)]
        [SerializeField] private int enemiesSpawnPercent;

        [SerializeField] private GameObject[] randomItems;
        [SerializeField] private GameObject[] randomTraps;
        [SerializeField] private Tilemap tilemap;

        [SerializeField] private DungeonType dungeonType;
        
        [SerializeField] protected internal GameObject wallPrefab;
        [SerializeField] private GameObject roomPrefab;
        [SerializeField] protected internal GameObject mainFloorTile;


        [SerializeField] protected internal Sprite[] floorTiles;
        
        [SerializeField] private bool useComplexWallTiles;
        [SerializeField] private GameObject[] wallTiles;

        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject exitDoorPrefab;
        [SerializeField] private GameObject colliderPrefab;
        
        [SerializeField] private LayerMask whatIsFloor;
        [SerializeField] private LayerMask whatIsWall;
        [SerializeField] private Tag tileSpawnerTag;
        
        [Range(10, 5000)]
        [SerializeField] private int totalFloorCount;
     
        [SerializeField] private bool useRoundedEdges;
        [SerializeField] private GameObject[] roundedEdges;

        [Header("For Dungeon with ROOMS")] 
        [Range(1,100)]
        [SerializeField] private int hallsFrequency = 50;
        [Range(1, 20)]
        [SerializeField] private int minHallLength = 5;
        [Range(1, 20)]
        [SerializeField] private int maxHallLength = 20;
 
        [SerializeField] private Vector2 minRoomSize = new Vector2(4,4);
        [SerializeField] private Vector2 maxRoomSize = new Vector2(7,7);
        
        [Header("Boss")]
        [SerializeField] private bool finalDungeonInWorld;
        [SerializeField] private int corridorLengthToBoss = 20;
        [SerializeField] private Vector2 bossRoomSize  = new Vector2(9,9);
        [SerializeField] private GameObject boss;


        [SerializeField] private GameObject playerSpawnPoint;

        protected internal float MinX, MaxX, MinY, MaxY;
        protected internal List<GameObject> _walls = new List<GameObject>();
        
        public List<GameObject> SpawnedEnemies { get; private set; }

        private List<Vector3> _floorList = new List<Vector3>();
        private List<Vector3> _roomCentresList = new List<Vector3>();
        private List<GameObject> _roomList = new List<GameObject>();
        private Vector2 _hitSize;
  
      

        private void Start()
        {
           
            
            
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
            Vector2 currentPos = Vector2.zero;
            _floorList.Add(currentPos);
            //set floor tile at current position
            while (_floorList.Count < totalFloorCount )
            {
                currentPos += Direction2D.GetRandomCardinalDirection();

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
            var roomIndex = 0;
            Vector3 currentPos = Vector3.zero;
            _floorList.Add(currentPos);
            //set floor tile at current position
            while (_floorList.Count < totalFloorCount )
            {
                int walkLength = Random.Range(minHallLength, maxHallLength);
                currentPos = GenerateLongWalk(currentPos, walkLength);
                GenerateRoom(currentPos, minRoomSize,maxRoomSize);
                roomIndex++;
            }
            playerSpawnPoint.transform.position = _roomList[0].transform.position;
            _roomList[0].SetActive(false);
            _roomList[_roomList.Count-1].SetActive(false);
            StartCoroutine(DelayedProgress());
 
        }

        private void StartWindingWalker()
        {
            var roomIndex = 0;
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
                    GenerateRoom(currentPos, minRoomSize,maxRoomSize);
                    roomIndex++;
                }
               
            }
            StartCoroutine(DelayedProgress());
        }


        private Vector3 GenerateLongWalk(Vector3 pos, int walkLength)
        {
            // creating halls
            Vector3 walkDirection = Direction2D.GetRandomCardinalDirection();
           
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
        private void GenerateRoom(Vector3 pos, Vector2 minSize, Vector2 maxSize)
        {
           
            //_roomList.Add(new List<Vector3>());
            int roomWidth = 0;
            int roomHeight = 0;
              // create a random room at the end of walk length
            roomWidth = Random.Range(Mathf.CeilToInt(minSize.x/2f), Mathf.CeilToInt(maxSize.x/2f)); // 4 tiles up and 4 tiles down relating to hallway (1) = 4 + 4 + 1
            roomHeight = Random.Range(Mathf.CeilToInt(minSize.y/2f), Mathf.CeilToInt(maxSize.y/2f));
            
         

           
            Vector3 center = new Vector3(Mathf.Floor(roomWidth/2), Mathf.Floor(roomHeight/2), 0);
            bool roomReady = false;
  
            for (int w = -roomWidth; w <= roomWidth; w++)
            {
                for (int h = -roomHeight; h <= roomHeight; h++)
                {
                    Vector3 offset = new Vector3(w, h, 0);
                    //Vector3 center = new Vector3(Mathf.Floor(w/2), Mathf.Floor(h/2), 0);
                    
                    if (!CheckIfInFloorList(pos + offset)) 
                    {
                        _floorList.Add(pos + offset);

                        if (w == roomWidth && h == roomHeight)
                        {
                           
                            _roomCentresList.Add(pos); 
                            GenerateRoomCollider((roomWidth * 2) + 1, (roomHeight * 2) + 1, pos);
                            
                           
                          
                        }
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
        private bool CheckIfInRoomCentreList(Vector3 tilePos)
        {
            //bool inFloorList = false;
            if (_roomCentresList.Contains(tilePos)) return true;
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

        
            
            GameActions.Current.DungeonGenerated();
            GameActions.Current.DungeonGeneratedToSaveMap(_floorList);
        }

        public void GenerateFloorTiles()
        {
            var randomColor = Random.ColorHSV(0,1,.2f,.7f, 1,1,1,1);
            tilemap.color = randomColor;
            // generating tiles (they change to floor)
            for (int i = 0; i < _floorList.Count; i++)
            {
                //Vector2Int targetTilePos = new Vector2Int((int)_floorList[i].x, (int)_floorList[i].y);
                //floorTilemap.SetTile((Vector3Int)targetTilePos, floorTile);
                
                GameObject tileGo = Instantiate(tilePrefab, _floorList[i], Quaternion.identity);
                GameActions.Current.TilePlacedTrigger(randomColor);
                tileGo.name = tilePrefab.name;
                tileGo.transform.SetParent(transform);
                
                //tileGo.GetComponent<SpriteRenderer>().material = materials[randomMaterialIndex];
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
                //Debug.Log(CheckAvailableSpaceForBossRoom(currentPos, bossRoomSize));
                GenerateRoom(currentPos, bossRoomSize, bossRoomSize);
                
                
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
        

        private IEnumerator InsertPlaceForExitDoor()
        {
            Vector2 dir = Direction2D.GetRandomCardinalDirection();
            bool isExitPlaced = IfExitIsPlaced(_floorList[_floorList.Count - 1]); // last tile
            while (!isExitPlaced)
            {
                Vector2 currentPos = WalkStraightInDir(_floorList[_floorList.Count - 1], dir);
              
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
                            
                    

                            SetRandomEnemies( hitFloor, hitTop,  hitRight,  hitBottom, hitLeft);
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
        
        private void SetRandomEnemies( Collider2D hitFloor, Collider2D hitTop,  Collider2D hitRight,  Collider2D hitBottom,  Collider2D hitLeft)
        {
            if (randomEnemies.Length > 0)
            {
                if ((hitTop || hitRight || hitBottom || hitLeft)
                    && !(hitTop && hitBottom)
                    && !(hitRight && hitLeft)){
                            
                    int roll = Random.Range(1, 101);
                    if (roll <= enemiesSpawnPercent)
                    {
                        int itemIndex = Random.Range(0, randomEnemies.Length);
                        GameObject itemGo = Instantiate(randomEnemies[itemIndex], hitFloor.transform.position,
                            Quaternion.identity);
                        SpawnedEnemies.Add(itemGo);
                        itemGo.name = randomEnemies[itemIndex].name;
                        itemGo.transform.SetParent(hitFloor.transform);
                    }
                }
            }
            //GameEventSystem.current.DungeonGenerated();
            
        }
        
        private void GenerateRoomCollider(int width, int height, Vector3 roomCenter)
        {
            GameObject roomCollider = Instantiate(colliderPrefab, roomCenter, Quaternion.identity);
            roomCollider.name = colliderPrefab.name;
            roomCollider.transform.SetParent(gameObject.transform);
            roomCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
            _roomList.Add(roomCollider);

        }
        private void OnDrawGizmos()
        {
           
            Gizmos.color = Color.magenta;
            foreach (Vector3 tile in _roomCentresList)
            {
              
                    Gizmos.DrawCube(tile, Vector3.one); 
                
            }
            
        }
    }
}