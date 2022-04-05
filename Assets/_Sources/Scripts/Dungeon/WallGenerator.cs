using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    public class WallGenerator : MonoBehaviour
    {
       
        [SerializeField] protected TileGenerator tileGenerator;
        [SerializeField] protected GameObject wallTilePrefab; 
        [SerializeField] protected GameObject[] wallTiles;
        [SerializeField] protected LayerMask whatIsWall;
        [SerializeField] protected LayerMask whatIsFloor;
        
        private AbstractDungeonGenerator _generator;
        private HashSet<Vector2> _wallPositions;
        private LayerMask _dungeonMask;
        private Vector2 _hitSize;

        private void Awake()
        {
            
            _generator = transform.parent.GetComponentInChildren<AbstractDungeonGenerator>();
            _dungeonMask = LayerMask.GetMask("Wall", "Floor");
            _hitSize = Vector2.one * 0.8f;
        }
        
        public void GenerateBasicWalls(HashSet<Vector2> floorPositions)
        {
         
            foreach (Vector2 floorTile in floorPositions)
            {
                GenerateWallsForSingleTile(floorTile);
            }
            
        }


        public void GenerateComplexWalls(float minX, float maxX, float minY, float maxY)
        {
            for (int x = (int)minX - 2; x < (int)maxX + 2; x++)
            {
                for (int y = (int)minY - 2; y < (int)maxY + 2; y++)
                {
                    //Debug.Log(x+", "+ y);
                    SetWallTiles(x, y);
                }
            }
        }

        private void GenerateWallsForSingleTile(Vector2 tile)
        {
           
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector2 targetTilePos = new Vector2(tile.x + x, tile.y + y);
                    Collider2D hit = Physics2D.OverlapBox(targetTilePos, Vector2.one * 0.8f, 0, _dungeonMask);
                    
                    if (!hit)
                    {
                        tileGenerator.InstantiateSingleTile(wallTilePrefab, targetTilePos, transform);
                    }
                }
            }
        }

        private void SetWallTiles(int x, int y)
        {
         
            Collider2D hitWall = Physics2D.OverlapBox(new Vector2(x, y), _hitSize, 0, whatIsWall);
            if (hitWall)
            {
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
                if (!hitTop && hitTopFloor) { bitValue += 1; }
                if (!hitRight && hitRightFloor) { bitValue += 2; }
                if (!hitBottom && hitBottomFloor) { bitValue += 4; }
                if (!hitLeft && hitLeftFloor) { bitValue += 8; }

       
                tileGenerator.InstantiateSingleTile(wallTiles[bitValue], new Vector2(x, y), hitWall.transform);
            
            }
        }
    }
    
}