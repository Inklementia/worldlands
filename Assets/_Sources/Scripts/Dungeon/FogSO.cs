using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    [CreateAssetMenu(fileName = "newFogData", menuName = "Data/Dungeon Data/Fog Data")]
    public class FogSO : ScriptableObject
    {
        public Tag PlayerTag;
        public float LightRadius = 5f;
        public float MaxVisibility = 10f;
        [Range(0, 1)] public float MaxOpacity;
        
        public LayerMask WhatIsWall;
        public LayerMask WhatIsFog;
    }
}