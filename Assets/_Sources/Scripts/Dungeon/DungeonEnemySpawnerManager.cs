using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public class DungeonEnemySpawnerManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] spawners;

        public void SetSpawner(Vector3 place, Transform parent)
        {
            Vector3 offset = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            int randomIndex = Random.Range(0, spawners.Length);
            GameObject spawnerGO = Instantiate(spawners[randomIndex], place+offset, Quaternion.identity);
            spawnerGO.name = spawners[randomIndex].name;
            spawnerGO.transform.SetParent(parent);
        } 
    }
}