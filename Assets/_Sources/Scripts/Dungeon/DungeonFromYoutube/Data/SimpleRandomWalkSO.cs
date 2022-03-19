using UnityEngine;

namespace _Sources.Scripts.Dungeon.DungeonFromYoutube.Data
{
    [CreateAssetMenu(fileName ="SimpleRandomWalkParameters_",menuName = "Dungeon/SimpleRandomWalkData")]
    public class SimpleRandomWalkSO : ScriptableObject
    {
        public int iterations = 10, walkLength = 10;
        public bool startRandomlyEachIteration = true;
    }
}
