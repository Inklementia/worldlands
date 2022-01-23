using UnityEngine;

namespace Dungeon
{
    [System.Serializable]
    public class ItemFrequency
    {
        public GameObject Prefab;
        [Range(0,100)]
        public float Frequency;

    }
}