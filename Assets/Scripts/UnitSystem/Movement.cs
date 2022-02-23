using System;
using System.Collections;
using System.Collections.Generic;
using MapSystem;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using PathFinding;

public class Movement : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private MapManager manager;
    //[SerializeField] public PathMovement pathMovement;
    public bool grabed;

    public Unit selectedUnit;
    private Vector3 center = new Vector3(0.5f , 0.5f , 0);
    
    //From Here pathfinding

    //[SerializeField] private Pathfinding2D _pathfinding2D;

    private void Start()
    {
        //pathMovement = FindObjectOfType<PathMovement>();
    }

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

        if (hitData)
        {
            selectedUnit = hitData.transform.gameObject.GetComponent<Unit>();

            if (selectedUnit.gameObject.CompareTag("Ally"))
            {
                //_pathfinding2D = selectedUnit.GetComponent<Pathfinding2D>();
                //pathMovement.seeker = selectedUnit.gameObject;
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
        }

        manager.dataFromTiles[clickedTile].position = gridPosition;
        
        if (selectedUnit.hasMoved != true)
        {

            //Move(selectedUnit, _pathfinding2D);
            
            selectedUnit.transform.DOMove(manager.dataFromTiles[clickedTile].position + center, 2);

            //unit.hasMoved = true;
            grabed = false;
        }
        else
        {
            return;
        }


        void Move(Unit selectedUnit, Pathfinding2D pathfinding2D)
        {
             for (int i = 0; i < pathfinding2D.path.Count; i++)
             {
                 Debug.Log(pathfinding2D.path[i].worldPosition);
                 selectedUnit.transform.DOMove(pathfinding2D.path[i].worldPosition, .5f, false);
            }
        }
    }
}
