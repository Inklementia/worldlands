using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Sources.Scripts.Dungeon
{
    public class TileGenerator : MonoBehaviour
    {
        [SerializeField] protected GameObject floorTilePrefab;
        private AbstractDungeonGenerator _generator;

        private void Awake()
        {
            _generator = transform.parent.GetComponentInChildren<AbstractDungeonGenerator>();
        }

        public void InstantiateFloorTiles(IEnumerable<Vector2> floorPositions)
        {
            InstantiateTiles(floorPositions, transform, floorTilePrefab);
            
        }

        private void InstantiateTiles(IEnumerable<Vector2> positions, Transform map, GameObject tile)
        {
            foreach (var position in positions)
            {
                InstantiateSingleTile(tile, position, map);
                AssignDungeonCoordinates(position);
            }
        }
        
        public void InstantiateSingleTile(GameObject tile, Vector2 place, Transform parent)
        {
            GameObject tileGo = Instantiate(tile, place, Quaternion.identity);

            tileGo.name = tile.name;
            tileGo.transform.SetParent(parent);
        }
        
        private void AssignDungeonCoordinates(Vector3 point)
        {
            if (point.x > _generator.maxX)
            {
                _generator.maxX = point.x;
            }
            if (point.x < _generator.minX)
            {
                _generator.minX = point.x;
            }
            if (point.y > _generator.maxY)
            {
                _generator.maxY = point.y;
            }
            if (point.y < _generator.minY)
            {
                _generator.minY = point.y;
            }
        }

    }
}