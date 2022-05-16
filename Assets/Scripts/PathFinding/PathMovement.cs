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
        [SerializeField] private UnitObstacle unitObstacle;
        [SerializeField] private Unit selectedUnit, enemyUnit;
        [SerializeField] private GameObject target;
        [SerializeField] private Tilemap map;
        [SerializeField] private CombatManager combatManager;
        [SerializeField] private bool grabbed, selectedNewSpace;
        
        //From here, TurnSystem

        [SerializeField] private TurnSystem.TurnSystem turnSystem;
        
        //From here, SoundSystem

        [SerializeField] private AudioManager source;
        
        private static readonly int Thickness = Shader.PropertyToID("_thickness");

        [SerializeField] private GameObject button;
        [SerializeField] private GameObject button2;
        [SerializeField] private GameObject ui;
        private Vector3 _lastPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !grabbed && !selectedNewSpace)
            {
                SelectUnit(); 
            }
            else if (Input.GetMouseButtonDown(0) && grabbed && !selectedNewSpace)
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
                grabbed = false;
                return;
            }
            if (hitData.transform.gameObject.CompareTag("Enemy"))
            {
                grabbed = false;
            }
            if (hitData.transform.gameObject.CompareTag("Ally"))
            {
                source.Play("SelectedUnit");
                
                selectedUnit = hitData.transform.gameObject.GetComponent<Unit>();
                pathMovement = selectedUnit.GetComponent<Pathfinding2D>();
                
                unitObstacle.UpdateObstacleMap();
                var unitPos = unitObstacle.obstacleTilemap.WorldToCell(selectedUnit.transform.position);
                if (unitObstacle.obstacleTilemap.GetTile(unitPos) != null)
                {
                    unitObstacle.obstacleTilemap.SetTile(unitPos, null);
                }
                pathMovement.UpdateGrid();

                selectedUnit.path.SetActive(true);
                selectedUnit.instancedMat.SetFloat(Thickness, 0.0016f);
                
                grabbed = true;
                

                if (selectedUnit.hasMoved)
                {
                    grabbed = false;
                    selectedUnit.path.SetActive(false);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
            }
        }

        private void SelectNewSpace()
        {
            if (grabbed) 
            {
                Vector2 worldPosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitData = Physics2D.Raycast(worldPosition, Vector2.zero, 0);
                
                if (!hitData)
                {
                    source.Play("SelectedSpace");
                
                    Vector2 mousePosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int gridPosition = map.WorldToCell(mousePosition);
                    
                    var newTarget = Instantiate(target, gridPosition, quaternion.identity);
                    selectedNewSpace = true;

                    if (map.GetTile(gridPosition) == null)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return; 
                    }

                    Vector3Int unitGridPos = map.WorldToCell(selectedUnit.transform.position);
                    Vector3Int targetGridPos = map.WorldToCell(newTarget.transform.position);

                    pathMovement.FindPath(unitGridPos, targetGridPos);

                     if (pathMovement.path == null)
                     {
                         grabbed = false;
                         selectedNewSpace = false;
                         Destroy(newTarget);
                         selectedUnit.path.SetActive(false);
                         selectedUnit.instancedMat.SetFloat(Thickness, 0);
                         return;
                     }
                    
                    if (pathMovement.path.Count > selectedUnit.movement)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return;
                    }
                    
                    MoveWithPath(pathMovement);
                    grabbed = false;
                    selectedNewSpace = false;
                    Destroy(newTarget);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
                else if (hitData.transform.gameObject.CompareTag("Enemy") && selectedUnit.className != "Sniper")
                {
                    source.Play("SelectedSpace");
                    
                    enemyUnit = hitData.transform.gameObject.GetComponent<Unit>();

                    Vector2 mousePosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 gridPosition = map.WorldToCell(mousePosition);
                        
                    var newTarget = Instantiate(target, gridPosition, quaternion.identity);
                    selectedNewSpace = true;
                    Vector3Int tilePosition = map.WorldToCell(mousePosition);
                    
                    if (map.GetTile(tilePosition) == null)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
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
                        grabbed = false;
                        selectedNewSpace = false;
                        Destroy(newTarget);
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        return;
                    }

                    if (pathMovement.path.Count > selectedUnit.movement + 1)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return;
                    }
                    
                    MoveToEnemyWithPath(pathMovement);
                    ui.SetActive(false);
                    button.SetActive(true);

                    grabbed = false;
                    selectedNewSpace = false;
                    Destroy(newTarget);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
                else if (hitData.transform.gameObject.CompareTag("Ally") && selectedUnit.unitName == "Juliet")
                {
                    source.Play("SelectedSpace");
                    
                    enemyUnit = hitData.transform.gameObject.GetComponent<Unit>();

                    if (enemyUnit.unitName == "Juliet")
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        return;
                    }

                    Vector2 mousePosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 gridPosition = map.WorldToCell(mousePosition);
                        
                    var newTarget = Instantiate(target, gridPosition, quaternion.identity);
                    selectedNewSpace = true;
                    Vector3Int tilePosition = map.WorldToCell(mousePosition);
                        
                    if (map.GetTile(tilePosition) == null)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
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
                        grabbed = false;
                        selectedNewSpace = false;
                        Destroy(newTarget);
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        return;
                    }
                    
                    if (pathMovement.path.Count > selectedUnit.movement + 1)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return;
                    }
                    
                    MoveToEnemyWithPath(pathMovement);
                    ui.SetActive(false);
                    button2.SetActive(true);

                    grabbed = false;
                    selectedNewSpace = false;
                    Destroy(newTarget);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
                else if(hitData.transform.gameObject.CompareTag("Enemy") && selectedUnit.className == "Sniper")
                {
                    source.Play("SelectedSpace");
                    
                    enemyUnit = hitData.transform.gameObject.GetComponent<Unit>();

                    Vector2 mousePosition = turnSystem.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 gridPosition = map.WorldToCell(mousePosition);
                        
                    var newTarget = Instantiate(target, gridPosition, quaternion.identity);
                    selectedNewSpace = true;
                    Vector3Int tilePosition = map.WorldToCell(mousePosition);
                    
                    if (map.GetTile(tilePosition) == null)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
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
                        grabbed = false;
                        selectedNewSpace = false;
                        Destroy(newTarget);
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        return;
                    }

                    if (pathMovement.path.Count > selectedUnit.movement + 1)
                    {
                        grabbed = false;
                        selectedNewSpace = false;
                        selectedUnit.path.SetActive(false);
                        selectedUnit.instancedMat.SetFloat(Thickness, 0);
                        Destroy(newTarget);
                        return;
                    }
                    
                    MoveToEnemyWithPathSniper(pathMovement);
                    ui.SetActive(false);
                    button.SetActive(true);

                    grabbed = false;
                    selectedNewSpace = false;
                    Destroy(newTarget);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
                else
                {
                    grabbed = false;
                    selectedUnit.path.SetActive(false);
                    selectedUnit.instancedMat.SetFloat(Thickness, 0);
                }
            }
            else
            {
                grabbed = false;
            }
        }

        private void MoveWithPath(Pathfinding2D unitPath)
        {
            selectedUnit.path.SetActive(false);

            var maxCount = unitPath.path.Count;

            var path = new Vector3[maxCount];
            for (var i = 0; i < path.Length; i++)
            {
                path[i] = unitPath.path[i].worldPosition - new Vector3(0.5f, 0.5f, 0);
            }

            selectedUnit.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
            
            selectedUnit.hasMoved = true;
        }

        private void MoveToEnemyWithPath(Pathfinding2D unitPath)
        {
            selectedUnit.path.SetActive(false);
            var maxCount = unitPath.path.Count - 1;
            
            var path = new Vector3[maxCount];
            for (var i = 0; i < path.Length; i++)
            {
                path[i] = unitPath.path[i].worldPosition - new Vector3(0.5f, 0.5f, 0);
            }

            selectedUnit.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
            
            selectedUnit.hasMoved = true;
        }
        
        private void MoveToEnemyWithPathSniper(Pathfinding2D unitPath)
        {
            selectedUnit.path.SetActive(false);
            var maxCount = unitPath.path.Count - 2;
            
            var path = new Vector3[maxCount];
            for (var i = 0; i < path.Length; i++)
            {
                path[i] = unitPath.path[i].worldPosition - new Vector3(0.5f, 0.5f, 0);
            }

            selectedUnit.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
            selectedUnit.hasMoved = true;
        }

        public void SendToCombat()
        {
            StartCoroutine(combatManager.MoveToCombat(selectedUnit, enemyUnit));
        }
        
        public void SendToHeal()
        {
            StartCoroutine(combatManager.MoveToCombat(selectedUnit, enemyUnit));
        }

        public void GoBack()
        {
            Vector3Int unitGridPos = map.WorldToCell(selectedUnit.transform.position);
            pathMovement.FindPath(unitGridPos, _lastPosition);
            MoveWithPath(pathMovement);
            
            selectedUnit.hasMoved = false;
            
            grabbed = false;
            selectedUnit.path.SetActive(false);
            selectedUnit.instancedMat.SetFloat(Thickness, 0);
            button.SetActive(false);
            button2.SetActive(false);
        }

        private void Deactivate()
        {
            selectedUnit.path.SetActive(false);
            selectedUnit.instancedMat.SetFloat(Thickness, 0);
            grabbed = false;
            selectedNewSpace = false;
        }
    }
}
