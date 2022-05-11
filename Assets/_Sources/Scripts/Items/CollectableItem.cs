using _Sources.Scripts.Interfaces;
using UnityEngine;

namespace _Sources.Scripts.Items
{
    [System.Serializable]
    public class CollectableItem
    {
        public GameObject prefab;
        public bool isUnique;
    }
}