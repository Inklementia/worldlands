using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public class RoomDungeonGenerator : AbstractDungeonGenerator
    {
      
        [SerializeField] private Vector2 minRoomSize = new Vector2(4, 4);
        [SerializeField] private Vector2 maxRoomSize = new Vector2(7, 7);
        [SerializeField] private bool isWinding = false;
        [Range(1, 100)] [SerializeField] private int hallsFrequency = 50;
        private List<Vector2> _floorPositions = new List<Vector2>();
        private List<Vector2> _roomCentresList = new List<Vector2>();
        private void Start()
        {
            GenerateDungeon();
        }

        protected override void RunProceduralGeneration()
        {

            RunRoomWalker(randomWalkParameters, startPosition);
            
            //RemoveFirstBattleAreaForPlayer();
            tileGenerator.InstantiateFloorTiles(_floorPositions);
            wallGenerator.GenerateBasicWalls(_floorPositions.ToHashSet());
            wallGenerator.GenerateComplexWalls((int) minX, (int) maxX, (int) minY, (int) maxY);
            SetExitDoor(_floorPositions);
            RemoveFirstBattleAreaForPlayer();
            RemoveIntersectRooms();
            RecalculateAStar();
            
            GameActions.Current.DungeonGeneratedToSaveMap(_floorPositions);
        }

        private void RunRoomWalker(RandomWalkerSO parameters, Vector2 position)
        {

            Vector3 currentPos = position;
            _floorPositions.Add(position);
            //set floor tile at current position
            while (_floorPositions.Count < parameters.TotalFloorCount)
            {
                if (isWinding)
                {
                    int roll = Random.Range(0, 100);
                    if (roll > hallsFrequency)
                    {
                        CreateSingleRoom(currentPos, minRoomSize, maxRoomSize);
                    }
                }
                else
                {
                    CreateSingleRoom(currentPos, minRoomSize, maxRoomSize);
                }
             
                int walkLength = Random.Range(parameters.MinWalkLength, parameters.MaxWalkLength);
                if (_floorPositions.Count + walkLength < parameters.TotalFloorCount)
                {
                    currentPos = GenerateLongWalk(currentPos, walkLength);
                    CreateSingleRoom(currentPos, minRoomSize, maxRoomSize);
                }
                
            }
        }


        private Vector3 GenerateLongWalk(Vector3 pos, int walkLength)
        {
            // creating halls
            Vector3 walkDirection = Direction2D.GetRandomCardinalDirection();

            for (int i = 0; i < walkLength; i++)
            {
                if (!_floorPositions.Contains(pos + walkDirection))
                {
                    _floorPositions.Add(pos + walkDirection);
                }

                pos += walkDirection;
            }

            return pos;
        }

        private void CreateSingleRoom(Vector3 position, Vector2 minSize, Vector2 maxSize)
        {
            //_roomList.Add(new List<Vector3>());
            int roomWidth = 0;
            int roomHeight = 0;
            // create a random room at the end of walk length
            roomWidth = Random.Range(Mathf.CeilToInt(minSize.x / 2f),
                Mathf.CeilToInt(maxSize.x / 2f)); // 4 tiles up and 4 tiles down relating to hallway (1) = 4 + 4 + 1
            roomHeight = roomWidth;


            for (int w = -roomWidth; w <= roomWidth; w++)
            {
                for (int h = -roomHeight; h <= roomHeight; h++)
                {
                    Vector3 offset = new Vector3(w, h, 0);
                    //Vector3 center = new Vector3(Mathf.Floor(w/2), Mathf.Floor(h/2), 0);

                    if (!_floorPositions.Contains(position + offset))
                    {
                        _floorPositions.Add(position + offset);

                        if (w == roomWidth && h == roomHeight)
                        {

                            _roomCentresList.Add(position); 
                            GenerateAreaCollider((roomWidth * 2) + 1, (roomHeight * 2) + 1, position);


                        }
                    }
                }
            }

        }

    }
}