using System;
using System.Collections;
using System.Collections.Generic;
using MapSystem;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private MapManager manager;

    public Ease myEase = Ease.Linear;

    public bool grabed;

    public Unit selectedUnit;
    private Vector3 center = new Vector3(0.5f , 0.5f , 0);
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !grabed)
        {
            SelectUnit(); 
        }
        else if (Input.GetMouseButtonDown(0) && grabed)
        {
            SelectNewSpace();
        }
    }

    void SelectUnit()
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
                grabed = true;
                // !! Update UI. !!
            }
            else if (selectedUnit.gameObject.CompareTag("Enemy"))
            {
                return;
            }
            else return;
        }
    }

    void SelectNewSpace()
    {
       
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        
        TileBase clickedTile = map.GetTile(gridPosition);

        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), Vector2.zero, 0);

        if (hitData)
        {
            Unit overlappedUnit = hitData.transform.gameObject.GetComponent<Unit>();

            if (overlappedUnit.gameObject.CompareTag("Ally"))
            {
                Debug.Log("ocupao");
                
                return;
                // !! Update UI. !!
            }
            else if (overlappedUnit.gameObject.CompareTag("Enemy"))
            {
                return;
            }
            else return;
        }

        manager.dataFromTiles[clickedTile].position = gridPosition;
        Debug.Log("Got new position" + manager.dataFromTiles[clickedTile].position);
        if (selectedUnit.hasMoved != true)
        {
            // selectedUnit.transform.position = manager.dataFromTiles[clickedTile].position + center;

            selectedUnit.transform.DOMove(manager.dataFromTiles[clickedTile].position + center, 2);

            //unit.hasMoved = true;
            grabed = false;
        }
        else
        {
            return;
        }

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


    }

    
}
