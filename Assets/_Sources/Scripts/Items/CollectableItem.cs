using _Sources.Scripts.Interfaces;
using UnityEngine;

namespace _Sources.Scripts.Items
{
    [System.Serializable]
    public class CollectableItem
    {
        public ICollectable collectable;
        public bool isUnique;
    }
}