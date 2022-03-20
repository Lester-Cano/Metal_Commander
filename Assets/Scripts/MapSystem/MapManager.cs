using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace MapSystem
{
    public class MapManager : MonoBehaviour
    {
        //Spawning Units.
        
        [SerializeField] public Tilemap allySpawners;
        [SerializeField] public Tilemap enemySpawners; 
        public List<Vector3> allySpawns; 
        public List<Vector3> enemySpawns;
        [SerializeField] public List<Unit> unitPrefab;
        [SerializeField] public List<Unit> enemyPrefab;
        private Random rnd = new Random();
        
        //TurnSystem Data

        [SerializeField] private TurnSystem.TurnSystem turnSystem;
        

        private void Start()
        { 
            allySpawners = GetComponent<Tilemap>();
            enemySpawners = GetComponent<Tilemap>(); 
            allySpawns = new List<Vector3>(); 
            enemySpawns = new List<Vector3>();
        }

        public void GetSpawners(Tilemap spawn, List<Vector3> spawners)
        {
            for (int n = spawn.cellBounds.xMin; n < spawn.cellBounds.xMax; n++)
            {
                for (int p = spawn.cellBounds.yMin; p < spawn.cellBounds.yMax; p++)
                {
                    Vector3Int localPlace = (new Vector3Int(n, p, (int) spawn.transform.position.y));
                    Vector3 place = spawn.CellToWorld(localPlace);
                    if (spawn.HasTile(localPlace))
                    {
                        //Tile at "place"
                        spawners.Add(place);
                    }
                    else
                    {
                        //No tile at "place"
                    }
                }
            }
        }

        public void SpawnUnit()
        {
            GetSpawners(allySpawners, allySpawns);
            for(var i = 0; i < allySpawns.Count; i++)
            {
                var newAllyUnit = Instantiate(unitPrefab[i], allySpawns[i], Quaternion.identity);
                turnSystem.allyTeam.Add(newAllyUnit);
            }
        }

        public void SpawnEnemies()
        {
            GetSpawners(enemySpawners, enemySpawns);
            for(var i = 0; i < enemySpawns.Count; i++)
            {
                int number = rnd.Next(0, 3);
                Debug.Log(number);
                var newEnemyUnit = Instantiate(enemyPrefab[number], enemySpawns[i], Quaternion.identity);
                turnSystem.enemyTeam.Add(newEnemyUnit);
            }
        }
    }
}
