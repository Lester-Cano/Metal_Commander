using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TurnSystem.States;
using UnityEngine.Serialization;
using CombatSystem;

namespace PathFinding
{

    public class EnemyMovement : MonoBehaviour
    {

        [SerializeField] private Pathfinding2D enemyMovement;
        [SerializeField] public List<Unit> enemies;
        [SerializeField] public List<Unit> allies;
        [SerializeField] private Unit currentEnemy;
        [SerializeField] private GameObject currentTarget = null;
        [SerializeField] private Unit currentPlayer;
        [SerializeField] public TurnSystem.TurnSystem turnSystem;

        [SerializeField] private CombatManager combatManager;

        public bool startCombat = false;

        [SerializeField] private bool rivalFound;
        

        private void Start()
        {
            FindEntities();
        }

        private void Update()
        {
            if (startCombat)
            {
                StartCoroutine(StartEnemy());
            }
        }
        
        private IEnumerator StartEnemy()
        {
            startCombat = false;
            
            for (var i = 0; i < enemies.Count; i++)
            {
                currentEnemy = enemies[i];

                if (currentEnemy.hitPoints > 0)
                {
                    enemyMovement = currentEnemy.GetComponent<Pathfinding2D>();
                    SearchForAllies();

                    if (currentEnemy.foundRival)
                    {
                        yield return new WaitForSeconds(4);
                    }

                    yield return new WaitForSeconds(0.1f);
                }
            }
            
            turnSystem.SetState(new PlayerTurnState(turnSystem));
        }
        
        private void FindEntities()
        {
            enemies = turnSystem.enemyTeam;
            allies = turnSystem.allyTeam;
        }
        
        private void SearchForAllies()
        {
            float nearest = 10000;
;
            foreach (var t in allies)
            {
                var actualMinDistance = Vector3.Distance(currentEnemy.transform.position, t.transform.position);

                if (actualMinDistance < nearest && t.hitPoints > 0)
                {
                    nearest = actualMinDistance;
                    currentTarget = t.gameObject;
                    currentPlayer = currentTarget.GetComponent<Unit>();
                    
                }
            }

            if (currentTarget != null && currentEnemy.inRange == true)
            {
                enemyMovement.FindPath(currentEnemy.transform.position, currentTarget.transform.position);
                
                if (enemyMovement.path.Count <= currentEnemy.movement)
                {
                    StartCoroutine(MoveInRange(enemyMovement));
                    
                    currentEnemy.foundRival = true;
                }
            }
            
            if (currentTarget != null && currentEnemy.passive == true)
            {
                //Do nothing.

                currentEnemy.foundRival = false;
            }
            
            if (currentTarget != null && currentEnemy.aggressive == true)
            {
                enemyMovement.FindPath(currentEnemy.transform.position, currentTarget.transform.position);
                StartCoroutine(MoveAggressive(enemyMovement));

                currentEnemy.foundRival = true;
            }
        }
        
        private IEnumerator MoveInRange(Pathfinding2D unitPath)
        {
            var maxCount = unitPath.path.Count - 1;
            var path = new Vector3[maxCount];
            for (var i = 0; i < path.Length; i++)
            {
                path[i] = unitPath.path[i].worldPosition;
            }
            
            var camPath = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++)
            {
                camPath[i] = path[i] + new Vector3(0, 0, -10);
            }

            turnSystem.mainCamera.transform.DOMove(currentEnemy.transform.position + new Vector3(0, 0, -10), 1,
                false);
            yield return new WaitForSeconds(1.1f);
            
            currentEnemy.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
            turnSystem.mainCamera.transform.DOPath(camPath, 1, PathType.Linear, PathMode.TopDown2D);

            if (currentEnemy.hitPoints > 0 && currentPlayer.hitPoints > 0)
            {
                EnemyCombat();
            }
        }
        
        private IEnumerator MoveAggressive(Pathfinding2D unitPath)
        {
            if (unitPath.path.Count - 1 <= currentEnemy.movement)
            {
                var maxCount = unitPath.path.Count - 1;
                var path = new Vector3[maxCount];
                for (var i = 0; i < path.Length; i++)
                {
                    path[i] = unitPath.path[i].worldPosition;
                }

                var camPath = new Vector3[path.Length];
                for (var i = 0; i < path.Length; i++)
                {
                    camPath[i] = path[i] + new Vector3(0, 0, -10);
                }

                turnSystem.mainCamera.transform.DOMove(currentEnemy.transform.position + new Vector3(0, 0, -10), 0.8f,
                    false);
                yield return new WaitForSeconds(1.1f);
                
                currentEnemy.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
                turnSystem.mainCamera.transform.DOPath(camPath, 1, PathType.Linear, PathMode.TopDown2D);

                if (currentEnemy.hitPoints > 0 && currentPlayer.hitPoints > 0)
                {
                    EnemyCombat();
                }
            }
            else
            {
                var maxCount = (int)currentEnemy.movement;
                var path = new Vector3[maxCount];
                for (var i = 0; i < maxCount; i++)
                {
                    path[i] = unitPath.path[i].worldPosition;
                }
                
                var camPath = new Vector3[path.Length];
                for (var i = 0; i < path.Length; i++)
                {
                    camPath[i] = path[i] + new Vector3(0, 0, -10);
                }

                turnSystem.mainCamera.transform.DOMove(currentEnemy.transform.position + new Vector3(0, 0, -10), 0.8f,
                    false);
                yield return new WaitForSeconds(1);
                
                currentEnemy.transform.DOPath(path, 1, PathType.Linear, PathMode.TopDown2D);
                turnSystem.mainCamera.transform.DOPath(camPath, 1, PathType.Linear, PathMode.TopDown2D);
            }
        }

        private void EnemyCombat()
        {
            currentTarget = null;
            
            if (currentPlayer != null)
            {
                StartCoroutine(combatManager.MoveToCombat(currentEnemy, currentPlayer));
            }

            currentEnemy.foundRival = false;
        }
    }
}