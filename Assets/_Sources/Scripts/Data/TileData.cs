using System;

namespace _Sources.Scripts.Data
{
    [Serializable]
    public class TileData
    {
        public float X;
        public float Y;

        public TileData(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}