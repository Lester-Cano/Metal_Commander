using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.Tilemaps;

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
                grabed = false;
                return;
            }
            if (hitData.transform.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Cant pick an enemy");
                grabed = false;
            }
            if (hitData.transform.gameObject.CompareTag("Ally"))
            {
                selectedUnit = hitData.transform.gameObject.GetComponent<Unit>();
                pathMovement = selectedUnit.GetComponent<Pathfinding2D>();
                
                selectedUnit.path.SetActive(true);
                
                grabed = true;
                
                if (selectedUnit.hasMoved == true)
                {
                    grabed = false;
                    selectedUnit.path.SetActive(false);
                    Debug.Log("Unit already acted");
                }
            }
        }

        void SelectNewSpace()
        {
            if (grabed == true)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 gridPosition = map.WorldToCell(mousePosition);

                var newTarget = Instantiate(target, gridPosition, quaternion.identity);
                selectedNewSpace = true;
            
                Vector3Int unitGridPos = map.WorldToCell(selectedUnit.transform.position);
                Vector3Int targetGridPos = map.WorldToCell(newTarget.transform.position);

                pathMovement.FindPath(unitGridPos, targetGridPos);

                if (pathMovement.path.Count > selectedUnit.movement)
                {
                    grabed = false;
                    selectedNewSpace = false;
                    selectedUnit.path.SetActive(false);
                    Destroy(newTarget);
                    return;
                }
            
                Move(pathMovement);

                grabed = false;
                selectedNewSpace = false;
                Destroy(newTarget);
            }
            else
            {
                grabed = false;
                return;
            }
        }

        private void Move(Pathfinding2D unitPath)
        {
            selectedUnit.path.SetActive(false);
             foreach (var t in unitPath.path)
             {
                 selectedUnit.transform.DOMove(t.worldPosition, 1.5f, true);
                 selectedUnit.hasMoved = true;
             }
        }
    }
}
