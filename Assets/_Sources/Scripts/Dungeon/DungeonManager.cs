using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Dungeon
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] randomItems;
        [SerializeField] protected internal GameObject wallPrefab;
        [SerializeField] protected internal GameObject floorPrefab;
        [SerializeField] protected internal GameObject tilePrefab;
        [SerializeField] protected internal GameObject exitDoorPrefab;
        [SerializeField] private LayerMask whatIsFloor;
        [SerializeField] private LayerMask whatIsWall;
        
        [Range(50, 5000)]
        [SerializeField] private int totalFloorCount;
        [Range(1, 100)]
        [SerializeField] private int itemSpawnPercent;
        
        [SerializeField] protected internal float MinX, MaxX, MinY, MaxY;
        
        

        private List<Vector3> _floorList = new List<Vector3>();

        private void Start()
        {
            StartRandomWalker();
        }

        private void Update()
        {
            // for testing
            if (Application.isEditor && Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void StartRandomWalker()
        {
            Vector3 currentPos = Vector3.zero;
            _floorList.Add(currentPos);
            //set floor tile at current position
            while (_floorList.Count < totalFloorCount)
            {
                switch (Random.Range(1, 5))
                {
                    case 1: currentPos += Vector3.up;
                        break;
                    case 2: currentPos += Vector3.right;
                        break;
                    case 3: currentPos += Vector3.down;
                        break;
                    case 4: currentPos += Vector3.left;
                        break;
                }

                bool inFloorList = false;
                for (int i = 0; i < _floorList.Count; i++)
                {
                    if (Vector3.Equals(currentPos, _floorList[i]))
                    {
                        inFloorList = true;
                        break;
                    }
                }

                if (!inFloorList) 
                {
                    _floorList.Add(currentPos);
                }
                
            }

            // generating tiles (they change to floor)
            for (int i = 0; i < _floorList.Count; i++)
            {
                GameObject tileGo = Instantiate(tilePrefab, _floorList[i], Quaternion.identity);
                tileGo.name = tilePrefab.name;
                tileGo.transform.SetParent(transform);
            }
            
            StartCoroutine(DelayedProgress());
           
            
        }


        private void SetExitDoor()
        {
            Vector3 doorPos = _floorList[_floorList.Count - 1];
            GameObject doorGo = Instantiate(exitDoorPrefab, doorPos, Quaternion.identity);
            doorGo.name = exitDoorPrefab.name;
            doorGo.transform.SetParent(transform);
        }

        private void CheckgeneratedDungeon ()
        {
            Vector2 hitSize = Vector2.one * 0.8f;
            for (int x = (int)MinX - 2; x< (int)MaxX + 2; x++)
            {
                for (int y = (int)MinY - 2; y< (int)MaxY + 2; y++)
                {
                    Collider2D hitFloor = Physics2D.OverlapBox(new Vector2(x, y), hitSize , 0, whatIsFloor);
                    if (hitFloor)
                    {
                     
                        // if not last generated tile
                        if (!Vector2.Equals(hitFloor.transform.position, _floorList[_floorList.Count - 1]))
                        {
                          
                            Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), hitSize , 0, whatIsWall);
                            Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), hitSize , 0, whatIsWall);
                            Collider2D hitBottom = Physics2D.OverlapBox(new Vector2(x, y - 1), hitSize , 0, whatIsWall);
                            Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), hitSize, 0, whatIsWall);

                            SetRandomItems( hitFloor, hitTop,  hitRight,  hitBottom, hitLeft);
                        }
                    }
                }
            }
        }

        private void SetRandomItems( Collider2D hitFloor, Collider2D hitTop,  Collider2D hitRight,  Collider2D hitBottom,  Collider2D hitLeft)
        {
            if ((hitTop || hitRight || hitBottom || hitLeft)
                && !(hitTop && hitBottom)
                && !(hitRight && hitLeft))
            {
                            
                int roll = Random.Range(0, 101);
                if (roll <= itemSpawnPercent)
                {
                    int itemIndex = Random.Range(0, randomItems.Length);
                    GameObject itemGo = Instantiate(randomItems[itemIndex], hitFloor.transform.position,
                        Quaternion.identity);
                    itemGo.name = randomItems[itemIndex].name;
                    itemGo.transform.SetParent(hitFloor.transform);
                }
                               
            }
        }
        private IEnumerator DelayedProgress()
        {
            if (FindObjectsOfType<TileSpawner>().Length > 0)
            {
                yield return null;
            }
            SetExitDoor();
            CheckgeneratedDungeon();
            
        }
    }
}