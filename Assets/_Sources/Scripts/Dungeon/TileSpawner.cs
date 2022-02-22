using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
   
    public class TileSpawner : MonoBehaviour
    {
        private DungeonManager _dungeonManager;
        private LayerMask _dungeonMask;
        
        
        private void Awake()
        {
            //_dungeonManager = gameObject.FindWithTag(_dungeonManagerTag).GetComponent<DungeonManager>();
            _dungeonManager = FindObjectOfType<DungeonManager>();

            GenerateFloor();
            AssignDungeonCoordinates();
        }

        private void Start()
        {
            _dungeonMask = LayerMask.GetMask("Wall", "Floor");
            GenerateWalls();
            // destroying spawner
            
            Destroy(gameObject);
        }

        private void AssignDungeonCoordinates()
        {
            if (transform.position.x > _dungeonManager.MaxX)
            {
                _dungeonManager.MaxX = transform.position.x;
            }
            if (transform.position.x < _dungeonManager.MinX)
            {
                _dungeonManager.MinX = transform.position.x;
            }
            if (transform.position.y > _dungeonManager.MaxY)
            {
                _dungeonManager.MaxY = transform.position.y;
            }
            if (transform.position.y < _dungeonManager.MinY)
            {
                _dungeonManager.MinY = transform.position.y;
            }
        }

        private void GenerateFloor()
        {
            //Vector2Int targetTilePos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            //_dungeonManager.floorTilemap.SetTile((Vector3Int)targetTilePos, _dungeonManager.floorTile);
            /* int randomIndex = Random.Range(0, _dungeonManager.otherFloorTiles.Length);
            float roll = Random.Range(0, 100);
            if (roll < _dungeonManager.otherFloorTiles[randomIndex].Frequency)
            {
                GameObject prefab = _dungeonManager.otherFloorTiles[randomIndex].Prefab;
                GameObject floorGo =
                    Instantiate(prefab, transform.position, Quaternion.identity);
                floorGo.name = prefab.name;
                floorGo.transform.SetParent(_dungeonManager.transform);
            }
            else
            {
                int randomIndex2 = Random.Range(0, _dungeonManager.mainFloorTiles.Length);
                GameObject prefab = _dungeonManager.mainFloorTiles[randomIndex2];
                GameObject floorGo =
                    Instantiate(prefab, transform.position, Quaternion.identity);
                floorGo.name = prefab.name;
                floorGo.transform.SetParent(_dungeonManager.transform);
            }
            */
            int randomIndex = Random.Range(0, _dungeonManager.floorTiles.Length);
            int rotateOrNotRotate = Random.Range(0, 2);
            //Debug.Log(rotateOrNotRotate);
           
            Quaternion rot;
            if (rotateOrNotRotate == 1)
            {
                rot = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                rot = Quaternion.identity;
            }
            
            GameObject prefab = _dungeonManager.mainFloorTile;

            prefab.GetComponent<SpriteRenderer>().sprite = _dungeonManager.floorTiles[randomIndex];
            GameObject floorGo =
                Instantiate(prefab, transform.position, rot);
            floorGo.name = prefab.name;
            floorGo.transform.SetParent(_dungeonManager.transform);
        }

        private void GenerateWalls()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                
                    
                    Vector2 targetTilePos = new Vector2(transform.position.x + x, transform.position.y + y);
                    Collider2D hit = Physics2D.OverlapBox(targetTilePos, Vector2.one * 0.8f, 0, _dungeonMask);
                    if (!hit)
                    {
                        
                            GameObject wallGo =
                                Instantiate(_dungeonManager.wallPrefab, targetTilePos, Quaternion.identity);
                            wallGo.name = _dungeonManager.wallPrefab.name;
                            wallGo.transform.SetParent(_dungeonManager.transform);
                        
                            _dungeonManager._walls.Add(wallGo);
                            
                            
                    }
                }
            }
            
            //_dungeonManager.CheckHorizontalWalls();
        }

   

        private void GenerateWalls2()
        {

                    Vector2 targetTilePos_TopLeft = new Vector2(transform.position.x - 1, transform.position.y + 1);
                    Vector2 targetTilePos_Top = new Vector2(transform.position.x + 0, transform.position.y + 1);
                    Vector2 targetTilePos_TopRight = new Vector2(transform.position.x + 1, transform.position.y + 1);
                    
                    Vector2 targetTilePos_Left = new Vector2(transform.position.x - 1, transform.position.y + 0);
                    Vector2 targetTilePos_Right = new Vector2(transform.position.x + 1, transform.position.y + 0);
     
                    
                    Vector2 targetTilePos_BottomLeft = new Vector2(transform.position.x - 1, transform.position.y - 1);
                    Vector2 targetTilePos_Bottom = new Vector2(transform.position.x + 0, transform.position.y - 1);
                    Vector2 targetTilePos_BottomRight = new Vector2(transform.position.x + 1, transform.position.y - 1);
                    
                    Collider2D hit_TopLeft = Physics2D.OverlapBox(targetTilePos_TopLeft, Vector2.one * 0.8f, 0, _dungeonMask);
                    Collider2D hit_Top = Physics2D.OverlapBox(targetTilePos_Top, Vector2.one * 0.8f, 0, _dungeonMask);
                    Collider2D hit_TopRight = Physics2D.OverlapBox(targetTilePos_TopRight, Vector2.one * 0.8f, 0, _dungeonMask);
                    
                    Collider2D hit_Left = Physics2D.OverlapBox(targetTilePos_Left, Vector2.one * 0.8f, 0, _dungeonMask);
                    Collider2D hit_Right = Physics2D.OverlapBox(targetTilePos_Right, Vector2.one * 0.8f, 0, _dungeonMask);
                    
                    Collider2D hit_BottomLeft = Physics2D.OverlapBox(targetTilePos_BottomLeft, Vector2.one * 0.8f, 0, _dungeonMask);
                    Collider2D hit_Bottom = Physics2D.OverlapBox(targetTilePos_Bottom, Vector2.one * 0.8f, 0, _dungeonMask);
                    Collider2D hit_BottomRight = Physics2D.OverlapBox(targetTilePos_BottomRight, Vector2.one * 0.8f, 0, _dungeonMask);

                    if (hit_Top)
                    {
                        
                    }
                 
                
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}