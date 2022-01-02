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
       
        [SerializeField] protected internal GameObject wallPrefab;
        [SerializeField] protected internal GameObject floorPrefab;
        [SerializeField] protected internal GameObject tilePrefab;
        [SerializeField] protected internal GameObject exitDoorPrefab;
        
        [SerializeField] private int totalFloorCount;
        
        protected internal float MinX, MaxX, MinY, MaxY;
        
        

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

            if (_floorList.Count == totalFloorCount)
            {
                SetExitDoor();
            }
            
        }


        private void SetExitDoor()
        {
            Vector3 doorPos = _floorList[_floorList.Count - 1];
            GameObject doorGo = Instantiate(exitDoorPrefab, doorPos, Quaternion.identity);
            doorGo.name = exitDoorPrefab.name;
            doorGo.transform.SetParent(transform);
        }
    }
}