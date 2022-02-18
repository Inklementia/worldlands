using UnityEngine;

namespace _Sources.Scripts.Data
{
    public static class DataExtensions
    {

        public static Vector3Data AsVectorData(this Vector3 vector) => new Vector3Data(vector.x, vector.y, vector.z);
        public static TileData AsTileData(this Vector2 vector) => new TileData(vector.x, vector.y);
    }
}