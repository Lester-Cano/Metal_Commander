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
    private Vector3 center = new Vector3(0.5f , 0.5f , 0);
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(SelectUnit());
        }
    }

    public IEnumerator SelectUnit()
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
            }
        }

        yield return new  WaitForSeconds(0.5f);
        
        StartCoroutine(SelectNewSpace());
    }

    public IEnumerator SelectNewSpace()
    {
        yield return new WaitForSeconds(2f);
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        

        TileBase clickedTile = map.GetTile(gridPosition);
        
        manager.dataFromTiles[clickedTile].position = gridPosition;
        Debug.Log("Got new position" + manager.dataFromTiles[clickedTile].position);
        MakeMove(selectedUnit, clickedTile);

        // if (manager.dataFromTiles[clickedTile].isOccupied == true)
        // {
        //     Debug.Log("Cannot step in occupied tile");
        // }
        // else
        // {
        //     manager.dataFromTiles[clickedTile].position = gridPosition;
        //     Debug.Log("Got new position" + manager.dataFromTiles[clickedTile].position);
        //     MakeMove(selectedUnit, clickedTile);
        // }
        
        yield return new WaitForSeconds(1f);
    }

    public void MakeMove(Unit unit, TileBase newPosition)
    {
        if (unit.hasMoved != true)
        {
            unit.transform.position = manager.dataFromTiles[newPosition].position + center;
            //unit.hasMoved = true;
        }
        else
        {
            return;
        }
    }
}
