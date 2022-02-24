using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace PathFinding
{
    public class PathMovement : MonoBehaviour
    {
        [SerializeField] private Pathfinding2D pathMovement;
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private GameObject target;
        [SerializeField] private Tilemap map;
        private bool grabed;
        private bool selectedNewSpace;

        //From here, TurnSystem

        [SerializeField] private TurnSystem.TurnSystem turnSystem;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !grabed && !selectedNewSpace)
            {
                SelectUnit(); 
            }
            else if (Input.GetMouseButtonDown(0) && grabed && !selectedNewSpace)
            {
                SelectNewSpace();
            }
        }

        private void SelectUnit()
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitData = Physics2D.Raycast(worldPosition, Vector2.zero, 0);

            if (!hitData)
            {
                return;
            }
            else
            {
                selectedUnit = hitData.transform.gameObject.GetComponent<Unit>();
                pathMovement = selectedUnit.GetComponent<Pathfinding2D>();
                grabed = true;

                if (selectedUnit.hasMoved == true)
                {
                    grabed = false;
                    Debug.Log("Unit already acted");
                }
            }
        }

        void SelectNewSpace()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 gridPosition = map.WorldToCell(mousePosition);

            var newTarget = Instantiate(target, gridPosition, quaternion.identity);
            selectedNewSpace = true;
            
            Debug.Log("Unit pos:" + selectedUnit.transform.position);
            Debug.Log("Target pos:" + newTarget.transform.position);
            
            pathMovement.FindPath(selectedUnit.transform.position, newTarget.transform.position);

            Move(pathMovement);

            grabed = false;
            selectedNewSpace = false;
            //Destroy(newTarget);
        }

        private void Move(Pathfinding2D unitPath)
        {
             foreach (var t in unitPath.path)
             {
                 Debug.Log(t.worldPosition);
                 selectedUnit.transform.DOMove(t.worldPosition, 2f, true);
                 selectedUnit.hasMoved = true;

                 StartCoroutine(selectedUnit.SearchCombat());
             }
        }
    }
}
