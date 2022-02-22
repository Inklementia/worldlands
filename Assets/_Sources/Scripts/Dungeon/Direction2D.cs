using System.Collections.Generic;
using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    public static class Direction2D
    {
        public static List<Vector2> cardinalDirectionsList = new List<Vector2>
        {
            new Vector2(0,1), //UP
            new Vector2(1,0), //RIGHT
            new Vector2(0, -1), // DOWN
            new Vector2(-1, 0) //LEFT
        };

        public static List<Vector2> diagonalDirectionsList = new List<Vector2>
        {
            new Vector2(1,1), //UP-RIGHT
            new Vector2(1,-1), //RIGHT-DOWN
            new Vector2(-1, -1), // DOWN-LEFT
            new Vector2(-1, 1) //LEFT-UP
        };

        public static List<Vector2> eightDirectionsList = new List<Vector2>
        {
            new Vector2(0,1), //UP
            new Vector2(1,1), //UP-RIGHT
            new Vector2(1,0), //RIGHT
            new Vector2(1,-1), //RIGHT-DOWN
            new Vector2(0, -1), // DOWN
            new Vector2(-1, -1), // DOWN-LEFT
            new Vector2(-1, 0), //LEFT
            new Vector2(-1, 1) //LEFT-UP

        };

        public static Vector2 GetRandomCardinalDirection()
        {
            return cardinalDirectionsList[UnityEngine.Random.Range(0, cardinalDirectionsList.Count)];
        }
    }
}