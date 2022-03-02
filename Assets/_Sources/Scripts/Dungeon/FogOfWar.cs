using System;
using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    public class FogOfWar : MonoBehaviour
    {
        [SerializeField] private GameObject fogPrefab;
        [SerializeField] private DungeonManager dungeonManager;

        private void Start()
        {
            GameActions.Current.OnDungeonGenerated += GenerateFog;
        }

        private void OnDisable()
        {
            GameActions.Current.OnDungeonGenerated -= GenerateFog;
        }

        private void SetFogTile(int x, int y)
        {
            Vector3 pos = new Vector3(x, y, 0);
            GameObject fogGO = Instantiate(fogPrefab, pos, Quaternion.identity, transform);
            fogGO.name = fogPrefab.name;
        }

        private void GenerateFog()
        {
            for (int x = (int) dungeonManager.MinX - 2; x < (int) dungeonManager.MaxX + 2; x++)
            {
                for (int y = (int) dungeonManager.MinY - 2; y < (int) dungeonManager.MaxY + 2; y++)
                {
                    SetFogTile(x, y);
                }
            }
        }
    }
}