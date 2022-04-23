using System;
using System.Collections;
using System.Collections.Generic;
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
        private bool _grabbed, _selectedNewSpace;

        //From here, TurnSystem

        [SerializeField] private TurnSystem.TurnSystem turnSystem;
        
        //From here, SoundSystem

        [SerializeField] private AudioManager source;

        public Ease easeIn = Ease.InExpo;
        public Ease easeOut = Ease.OutExpo;
        private static readonly int Walk2 = Animator.StringToHash("Walk2");

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_grabbed && !_selectedNewSpace)
            {
                SelectUnit(); 
            }
            else if (Input.GetMouseButtonDown(0) && _grabbed && !_selectedNewSpace)
            {
                SelectNewSpace();
            }
        }

        private void SelectUnit()
        {
            Vector2 worldPosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitData = Physics2D.Raycast(worldPosition, Vector2.zero, 0);

            if (!hitData)
            {
                _grabbed = false;
                return;
            }
            if (hitData.transform.gameObject.CompareTag("Enemy"))
            {
                _grabbed = false;
            }
            if (hitData.transform.gameObject.CompareTag("Ally"))
            {
                source.Play("SelectedUnit");
                
                selectedUnit = hitData.transform.gameObject.GetComponent<Unit>();
                pathMovement = selectedUnit.GetComponent<Pathfinding2D>();
                
                selectedUnit.path.SetActive(true);
                
                _grabbed = true;
                
                //turnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + selectedUnit.transform.position, 0.2f, false);
                selectedUnit.anim.SetBool("Walk2", true);

                if (selectedUnit.hasMoved)
                {
                    _grabbed = false;
                    selectedUnit.path.SetActive(false);
                    
                    selectedUnit.anim.SetBool("Walk2", false);
                }
            }
        }

        void SelectNewSpace()
        {
            if (_grabbed)
            {
                Vector2 worldPosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitData = Physics2D.Raycast(worldPosition, Vector2.zero, 0);
                
                if (!hitData)
                {
                    source.Play("SelectedSpace");
                
                    Vector2 mousePosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 gridPosition = map.WorldToCell(mousePosition);

                    var newTarget = Instantiate(target, gridPosition + new Vector3(0.5f, 0.5f, 0f), quaternion.identity);
                    _selectedNewSpace = true;

                    Vector3Int tilePosition = map.WorldToCell(mousePosition);

                    if (map.GetTile(tilePosition) == null)
                    {
                        _grabbed = false;
                        _selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.anim.SetBool("Walk2", false);
                        Destroy(newTarget);
                        return;
                    }

                    Vector3Int unitGridPos = map.WorldToCell(selectedUnit.transform.position);
                    Vector3Int targetGridPos = map.WorldToCell(newTarget.transform.position);

                    pathMovement.FindPath(unitGridPos, targetGridPos);

                    if (pathMovement.path == null)
                    {
                        _grabbed = false;
                        _selectedNewSpace = false;
                        Destroy(newTarget);
                        selectedUnit.path.SetActive(false);
                        return;

                    }
                    if (pathMovement.path.Count > selectedUnit.movement)
                    {
                        _grabbed = false;
                        _selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.anim.SetBool(Walk2, false);
                        Destroy(newTarget);
                        return;
                    }
                    
                    MoveWithPath(pathMovement);

                    _grabbed = false;
                    _selectedNewSpace = false;
                    Destroy(newTarget);
                }
                else
                {
                    _grabbed = false;
                    selectedUnit.path.SetActive(false);
                    
                    selectedUnit.anim.SetBool(Walk2, false);
                }
            }
            else
            {
                _grabbed = false;
            }
        }

        private IEnumerator Move(Pathfinding2D unitPath)
        {
            selectedUnit.path.SetActive(false);
            
             foreach (var t in unitPath.path)
             {
                 selectedUnit.transform.DOMove(t.worldPosition, 0.2f, true);
                 yield return new WaitForSeconds(0.2f);
             }

            selectedUnit.anim.SetBool(Walk2, false);
             
            selectedUnit.hasMoved = true;

            yield return new WaitForSeconds(0);
        }

        private void MoveWithPath(Pathfinding2D unitPath)
        {
            selectedUnit.path.SetActive(false);

            var maxCount = unitPath.path.Count;

            var path = new Vector3[maxCount];
            for (var i = 0; i < path.Length; i++)
            {
                path[i] = unitPath.path[i].worldPosition;
            }

            selectedUnit.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
            
            selectedUnit.anim.SetBool(Walk2, false);
            selectedUnit.hasMoved = true;
        }
    }
}
