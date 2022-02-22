﻿using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public static class ProceduralGenerationAlgorithms
    {
    
        public static HashSet<Vector2> SimpleRandomWalk(Vector2 startPosition, int walkLength)
        {
            HashSet<Vector2> path = new HashSet<Vector2>();

            path.Add(startPosition);
            var previousPosition = startPosition;

            for (int i = 0; i < walkLength; i++)
            {
                var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
                path.Add(newPosition);
                previousPosition = newPosition;
            }
            return path;
        }

        public static List<Vector2> RandomWalkCorridor(Vector2 startPosition, int corridorLength)
        {
            List<Vector2> corridor = new List<Vector2>();
            var direction = Direction2D.GetRandomCardinalDirection();
            var currentPosition = startPosition;
            corridor.Add(currentPosition);

            for (int i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition);
            }
            return corridor;
        }

        public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
        {
            Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
            List<BoundsInt> roomsList = new List<BoundsInt>();
            roomsQueue.Enqueue(spaceToSplit);
            while(roomsQueue.Count > 0)
            {
                var room = roomsQueue.Dequeue();
                if(room.size.y >= minHeight && room.size.x >= minWidth)
                {
                    if(Random.value < 0.5f)
                    {
                        if(room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }else if(room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minWidth, roomsQueue, room);
                        }else if(room.size.x >= minWidth && room.size.y >= minHeight)
                        {
                            roomsList.Add(room);
                        }
                    }
                    else
                    {
                        if (room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minWidth, roomsQueue, room);
                        }
                        else if (room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth && room.size.y >= minHeight)
                        {
                            roomsList.Add(room);
                        }
                    }
                }
            }
            return roomsList;
        }

        private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var xSplit = Random.Range(1, room.size.x);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }

        private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var ySplit = Random.Range(1, room.size.y);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }
    }
}