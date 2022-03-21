using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    public abstract class AbstractDungeonGenerator : MonoBehaviour
    {
        [SerializeField] protected TileGenerator tileGenerator = null;
        [SerializeField] protected WallGenerator wallGenerator = null;
        [SerializeField] protected Vector2 startPosition = Vector2.zero;
        [SerializeField] protected RandomWalkerSO randomWalkParameters;
        [SerializeField] private GameObject colliderPrefab;
        [SerializeField] private DungeonEnemySpawnerManager spawnerManager;
        [SerializeField] private GameObject exitDoorPrefab;


        [HideInInspector] public float minX, maxX, minY, maxY;

        private List<GameObject> _roomList = new List<GameObject>();
        [SerializeField] private Tag worldManagerTag;
        protected WorldManager WorldManager;
        protected virtual void Awake()
        {
            //WorldManager = gameObject.FindWithTag(worldManagerTag).GetComponent<WorldManager>();
            WorldManager = GetComponentInParent<WorldManager>();
        }

        protected virtual void Start()
        {
           
        }

        protected virtual void GenerateDungeon()
        {

        }

        protected abstract void RunProceduralGeneration();

        protected void GenerateAreaCollider(int width, int height, Vector3 roomCenter)
        {
            
            GameObject roomCollider = Instantiate(colliderPrefab, roomCenter, Quaternion.identity);
            roomCollider.name = colliderPrefab.name;
            roomCollider.transform.SetParent(gameObject.transform);
            roomCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, height);

            //set Spawner
            spawnerManager.SetSpawner(roomCenter, roomCollider.transform);

            // for room generator
            _roomList.Add(roomCollider);

        }

        protected void RemoveIntersectRooms()
        {
            for (int i = 0; i < _roomList.Count-1; i++)
            {
                if (_roomList[i].GetComponent<BoxCollider2D>().bounds
                    .Intersects(_roomList[i + 1].GetComponent<BoxCollider2D>().bounds))
                {
                    Debug.Log("Bounds intersecting");
                    Destroy(_roomList[i]);
                }
            }
        }
        private void DeactivateArea(int roomIndex)
        {
            _roomList[roomIndex].SetActive(false);
            Destroy(_roomList[roomIndex]);
        }

        protected void SetExitDoor(List<Vector2> floorPosition)
        {
            Vector3 doorPos = floorPosition[floorPosition.Count - 1];
            GameObject doorGo = Instantiate(exitDoorPrefab, doorPos, Quaternion.identity);
            doorGo.name = exitDoorPrefab.name;
            doorGo.transform.SetParent(transform);
        }

        protected void RemoveFirstBattleAreaForPlayer()
        {
            for (int i = 0; i < _roomList.Count; i++)
            {
                if (_roomList[i].transform.position == (Vector3) startPosition)
                {
                    DeactivateArea(i);
                }

                for (int x = -2; x <= 2; x++)
                {
                    for (int y = -2; y <=2; y++)
                    {
                        if (_roomList[i].GetComponent<BoxCollider2D>().bounds.Contains(startPosition + new Vector2(x,y)))
                        {
                            DeactivateArea(i);
                        }
                    }
                }
              
            }
        }

        protected void RecalculateAStar()
        {
            // Recalculate only the first grid graph
            var graphToScan = AstarPath.active.data.gridGraph;
            int width = (int) Mathf.Abs(minX - maxX) + 1;
            int depth = (int) Mathf.Abs(minY - maxY) + 1;
            var nodeSize = 1f;
            graphToScan.SetDimensions(width, depth, nodeSize);
            graphToScan.center.x = (minX + maxX) / 2;
            graphToScan.center.y = (minY + maxY) / 2;
            AstarPath.active.Scan(graphToScan);
        }
    }
}