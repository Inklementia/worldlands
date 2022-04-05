using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public class TileGenerator : MonoBehaviour
    {
        [SerializeField] protected GameObject floorTilePrefab;
        private AbstractDungeonGenerator _generator;
        [SerializeField] protected internal Sprite[] floorTiles;
        private void Awake()
        {
            _generator = transform.parent.GetComponentInChildren<AbstractDungeonGenerator>();
        }

        public void InstantiateFloorTiles(IEnumerable<Vector2> floorPositions)
        {
            InstantiateTiles(floorPositions, transform, floorTilePrefab, true);
            
        }

        private void InstantiateTiles(IEnumerable<Vector2> positions, Transform map, GameObject tile, bool isFloor)
        {
            foreach (var position in positions)
            {
                if (isFloor)
                {
                    InstantiateSingleFloorTile(tile, position, map);
                }
                else
                {
                    InstantiateSingleTile(tile, position, map);
                }
                AssignDungeonCoordinates(position);
            }
        }
        
        public void InstantiateSingleTile(GameObject tile, Vector2 place, Transform parent)
        {
            GameObject tileGo = Instantiate(tile, place, Quaternion.identity);

            tileGo.name = tile.name;
            tileGo.transform.SetParent(parent);
        }
        public void InstantiateSingleFloorTile(GameObject tile, Vector2 place, Transform parent){
            
            int randomIndex = Random.Range(0, floorTiles.Length);
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
            tile.GetComponent<SpriteRenderer>().sprite = floorTiles[randomIndex];
            GameObject tileGo = Instantiate(tile, place, rot);

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