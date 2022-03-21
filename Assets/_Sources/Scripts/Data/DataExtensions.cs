using UnityEngine;

namespace _Sources.Scripts.Data
{
    public static class DataExtensions
    {

        public static Vector3Data AsVectorData(this Vector3 vector) => new Vector3Data(vector.x, vector.y, vector.z);
        public static Vector2Data AsTileData(this Vector2 vector) => new Vector2Data(vector.x, vector.y);

        public static Vector3 AsUnityVector3(this Vector3Data vector3data) =>
            new Vector3(vector3data.X, vector3data.Y, vector3data.Z);
        public static Vector2 AsUnityVector2(this Vector2Data vector2data) =>
            new Vector2(vector2data.X, vector2data.Y);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        public static T ToDeserialized<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}