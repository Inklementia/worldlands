using System;
using UnityEngine;

namespace Dungeon
{
    public class Node
    {
        public Vector2 Position;
        public Vector2 Parent;

        public Node(Vector2 position, Vector2 parent)
        {
            Position = position;
            Parent = parent;
        }
    }
}