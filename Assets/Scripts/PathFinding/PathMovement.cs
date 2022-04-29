using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using CombatSystem;

namespace PathFinding
{
    public class PathMovement : MonoBehaviour
    {
        [SerializeField] private Pathfinding2D pathMovement;
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private Unit enemyUnit;
        [SerializeField] private GameObject target;
        [SerializeField] private Tilemap map;
        [SerializeField] private CombatManager combatManager;
        private bool _grabbed, _selectedNewSpace;

        //From here, TurnSystem

        [SerializeField] private TurnSystem.TurnSystem turnSystem;
        
        //From here, SoundSystem

        [SerializeField] private AudioManager source;
        
        private static readonly int Walk2 = Animator.StringToHash("Walk2");
        private static readonly int Thickness = Shader.PropertyToID("_thickness");

        [SerializeField] private GameObject button;
        [SerializeField] private GameObject UI;
        private Vector3 _lastPosition;

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
                selectedUnit.instancedMat.SetFloat(Thickness, 0.0016f);
                
                _grabbed = true;
                
                selectedUnit.anim.SetBool(Walk2, true);

                if (selectedUnit.hasMoved)
                {
                    _grabbed = false;
                    selectedUnit.path.SetActive(false);
                    
                    selectedUnit.anim.SetBool(Walk2, false);
                    
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
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
                        selectedUnit.anim.SetBool(Walk2, false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
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
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        return;
                    }
                    
                    if (pathMovement.path.Count > selectedUnit.movement)
                    {
                        _grabbed = false;
                        _selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.anim.SetBool(Walk2, false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return;
                    }
                    
                    MoveWithPath(pathMovement);

                    _grabbed = false;
                    _selectedNewSpace = false;
                    Destroy(newTarget);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
                else if (hitData.transform.gameObject.CompareTag("Enemy"))
                {
                    source.Play("SelectedSpace");
                    
                    enemyUnit = hitData.transform.gameObject.GetComponent<Unit>();

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
                        selectedUnit.anim.SetBool(Walk2, false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return;
                    }
                        
                    Vector3Int unitGridPos = map.WorldToCell(selectedUnit.transform.position);
                    Vector3Int targetGridPos = map.WorldToCell(newTarget.transform.position);

                    _lastPosition = unitGridPos;
                        
                    pathMovement.FindPath(unitGridPos, targetGridPos);
                        
                    if (pathMovement.path == null)
                    {
                        _grabbed = false;
                        _selectedNewSpace = false;
                        Destroy(newTarget);
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        return;
                    }
                    
                    if (pathMovement.path.Count > selectedUnit.movement)
                    {
                        _grabbed = false;
                        _selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.anim.SetBool(Walk2, false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return;
                    }
                    
                    MoveToEnemyWithPath(pathMovement);
                    UI.SetActive(false);
                    button.SetActive(true);

                    _grabbed = false;
                    _selectedNewSpace = false;
                    Destroy(newTarget);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
                else
                {
                    _grabbed = false;
                    selectedUnit.path.SetActive(false);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                    selectedUnit.anim.SetBool(Walk2, false); 
                }
            }
            else
            {
                _grabbed = false;
            }
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

        private void MoveToEnemyWithPath(Pathfinding2D unitPath)
        {
            selectedUnit.path.SetActive(false);
            var maxCount = unitPath.path.Count - 1;
            
            var path = new Vector3[maxCount];
            for (var i = 0; i < path.Length; i++)
            {
                path[i] = unitPath.path[i].worldPosition;
            }

            selectedUnit.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
            
            selectedUnit.anim.SetBool(Walk2, false);
            selectedUnit.hasMoved = true;
        }

        public void SendToCombat()
        {
            StartCoroutine(combatManager.MoveToCombat(selectedUnit, enemyUnit));
        }

        public void GoBack()
        {
            Vector3Int unitGridPos = map.WorldToCell(selectedUnit.transform.position);
            pathMovement.FindPath(unitGridPos, _lastPosition);
            MoveWithPath(pathMovement);
            
            button.SetActive(false);
        }
    }
}
