using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    SetMap map;
    [SerializeField] 
    Unit curUnit;

    [SerializeField]
    Tile start, end;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AttemptUnitMove(start, end);
        }
    }

    public bool AttemptUnitMove(Tile start, Tile end)
    {
        if (start.isOccupied == true && end.isWalkable && end.isOccupied == false)
        {
            curUnit.transform.position = end.transform.position;
            start.isOccupied = false;
            end.isOccupied = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
