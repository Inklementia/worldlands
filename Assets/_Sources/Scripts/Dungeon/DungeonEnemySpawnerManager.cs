using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public class DungeonEnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] spawners;

        public void SetSpawner(Vector3 place, Transform parent)
        {
            int randomIndex = Random.Range(0, spawners.Length);
            GameObject spawnerGO = Instantiate(spawners[randomIndex], place, Quaternion.identity);
            spawnerGO.name = spawners[randomIndex].name;
            spawnerGO.transform.SetParent(parent);
        } 
    }
}