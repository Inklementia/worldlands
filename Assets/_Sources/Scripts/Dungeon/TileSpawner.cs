using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
   
    public class TileSpawner : MonoBehaviour
    {
        private DungeonManager _dungeonManager;
        private LayerMask _dungeonMask;
        //private Color _color;

        private void OnEnable()
        {
        
        }

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
            //Debug.Log(_color);
            
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
            //floorGo.GetComponent<SpriteRenderer>().color = _color;
            _dungeonManager._floors.Add(floorGo);
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
                            //wallGo.GetComponent<SpriteRenderer>().color = _color;
                            _dungeonManager._walls.Add(wallGo);
                            
                            
                    }
                }
            }
            
            //_dungeonManager.CheckHorizontalWalls();
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}