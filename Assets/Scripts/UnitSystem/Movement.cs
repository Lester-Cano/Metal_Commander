using System;
using System.Collections;
using System.Collections.Generic;
using MapSystem;
using UnityEngine;
using  UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private MapManager manager;

    public Unit selectedUnit;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }
    }

    public void SelectUnit()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);

        TileBase clickedTile = map.GetTile(gridPosition);

        if (hitData)
        {
            selectedUnit = hitData.transform.gameObject.GetComponent<Unit>();

            if (selectedUnit.gameObject.CompareTag("Ally"))
            {
                // !! Update UI. !!
            }
            else if (selectedUnit.gameObject.CompareTag("Enemy"))
            {
                // !! Update UI. !!
                SelectUnit();
            }
        }
        
        SelectNewSpace();
    }

    public void SelectNewSpace()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);

        TileBase clickedTile = map.GetTile(gridPosition);

        if (manager.dataFromTiles[clickedTile].isOccupied == true)
        {
            Debug.Log("Cannot step in occupied tile");
            SelectUnit();
        }
        else
        {
            manager.dataFromTiles[clickedTile].position = gridPosition;
            Debug.Log("Got new position" + manager.dataFromTiles[clickedTile].position);
        }
    }

    public void MakeMove(Unit selectedUnit, TileBase newPosition)
    {
        if (selectedUnit.hasMoved != true)
        {
            selectedUnit.transform.position = manager.dataFromTiles[newPosition].position;
            selectedUnit.hasMoved = true;
        }
        else
        {
            return;
        }
    }
}
