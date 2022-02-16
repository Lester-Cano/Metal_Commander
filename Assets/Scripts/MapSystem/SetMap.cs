using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMap : MonoBehaviour
{
    [SerializeField]
    Tile[] tileMap;
    [SerializeField]
    Unit unitPrefab;
    private Tile spawn;

    public void SpawnUnit()
    {
        for(int i = 0; i < 10; i++)
        {
            if(tileMap[i].isSpawn == true)
            {
                Instantiate(unitPrefab, tileMap[i].transform.position, Quaternion.identity);
                tileMap[i].isOccupied = true;
            }
        }
    }
}
