using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetMap : MonoBehaviour
{
    [SerializeField] public Tilemap allySpawners;
    [SerializeField] public Tilemap enemySpawners;
    public List<Vector3> allySpawns;
    public List<Vector3> enemySpawns;
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private Unit enemyPrefab;

    private Vector3 center = new Vector3(0.5f , 0.5f , 0);

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
            Instantiate(unitPrefab, allySpawns[i] + center, Quaternion.identity);
        }
    }

    public void SpawnEnemies()
    {
        GetSpawners(enemySpawners, enemySpawns);
        for(var i = 0; i < enemySpawns.Count; i++)
        {
            Instantiate(enemyPrefab, enemySpawns[i] + center, Quaternion.identity);
        }
    }
}
