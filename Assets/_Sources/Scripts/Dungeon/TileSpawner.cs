using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
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
            GameObject floorGo =
                Instantiate(_dungeonManager.floorPrefab, transform.position, Quaternion.identity);
            floorGo.name = _dungeonManager.floorPrefab.name;
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
                        //add a wall
                        
                        GameObject wallGo =
                            Instantiate(_dungeonManager.wallPrefab, targetTilePos, Quaternion.identity);
                        wallGo.name = _dungeonManager.wallPrefab.name;
                        wallGo.transform.SetParent(_dungeonManager.transform);
                    }
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}