using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    [System.Serializable]
    public class ItemFrequency
    {
        public GameObject Prefab;
        [Range(0,100)]
        public float Frequency;

    }
}