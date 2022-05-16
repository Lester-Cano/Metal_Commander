using System.Collections;
using System.Collections.Generic;
using CombatSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitObstacle : MonoBehaviour
{
    [SerializeField] private TurnSystem.TurnSystem turnSystem;
    [SerializeField] public List<Unit> units;
    [SerializeField] public Tilemap obstacleTilemap;
    [SerializeField] private Tile tile;

    public void UpdateObstacleMap()
    {
        if (units != null)
        {
            foreach (var t in units)
            {
                var position = t.transform.position;
                var intPosition = Vector3Int.FloorToInt(position);
                obstacleTilemap.SetTile(intPosition, null);
            }
            
            units.Clear();
        }
        foreach (var t in turnSystem.enemyTeam)
        {          
            units?.Add(t);
        }
        foreach (var t in turnSystem.allyTeam)
        {          
            units?.Add(t);
        }

        foreach (var t in units)
        {
            var position = t.transform.position;
            var intPosition = Vector3Int.FloorToInt(position);
            obstacleTilemap.SetTile(intPosition, tile);
        }
    }
}
