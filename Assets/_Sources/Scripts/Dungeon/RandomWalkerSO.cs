using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    [CreateAssetMenu(fileName = "newDungeonWalkerData", menuName = "Data/Dungeon Data/Random Walker Data")]
    public class RandomWalkerSO : ScriptableObject
    {
        [Range(50, 5000)]
        public int TotalFloorCount = 500;

        [Range(1, 20)] [SerializeField] public int MinWalkLength = 5;
        [Range(1, 20)] [SerializeField] public int MaxWalkLength = 20;
        
    }
}