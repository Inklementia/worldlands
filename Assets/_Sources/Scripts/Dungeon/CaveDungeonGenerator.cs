using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public class CaveDungeonGenerator : AbstractDungeonGenerator
    {
        private HashSet<Vector2> _floorPositions = new HashSet<Vector2>();
        [Range(1, 100)]
        [SerializeField] private float enemySpawnersFrequency = 1;
        [Range(1, 100)]
        [SerializeField] private float enemiesFrequency = 1;
        private void Start()
        {
            GenerateDungeon();
        }

        protected override void RunProceduralGeneration()
        {
            RunRandomWalk(randomWalkParameters, startPosition);
          
            tileGenerator.InstantiateFloorTiles(_floorPositions); 
            wallGenerator.GenerateBasicWalls(_floorPositions);
            wallGenerator.GenerateComplexWalls((int)minX,(int)maxX, (int)minY, (int)maxY);
            SetExitDoor(_floorPositions.ToList());
            RemoveFirstBattleAreaForPlayer();
            RecalculateAStar();
        }

        private void RunRandomWalk(RandomWalkerSO parameters, Vector2 position)
        {
            var currentPosition = position;
            _floorPositions.Add(currentPosition);
            while (_floorPositions.Count < parameters.TotalFloorCount)
            {
                currentPosition +=  Direction2D.GetRandomCardinalDirection();
                //int walkLength = Random.Range(parameters.MinWalkLength, parameters.MaxWalkLength);
               // var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.MinWalkLength);
               // _floorPositions.UnionWith(path);
                if (!_floorPositions.Contains(currentPosition))
                    _floorPositions.Add(currentPosition);
                
                GenerateEnemyAreas(currentPosition);
            }
            
        }

        private void GenerateEnemyAreas(Vector2 currentPosition)
        {
            float roll = Random.Range(0, 100);
            if (roll < enemySpawnersFrequency)
            {
                GenerateAreaCollider(3, 3, currentPosition);
            }
        }
    }
}