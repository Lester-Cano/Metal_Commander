using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TurnSystem.States;
using UnityEngine.Serialization;

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
            
            for (int i = 0; i < enemies.Count; i++)
            {
                currentEnemy = enemies[i];

                if (currentEnemy.hitPoints > 0)
                {
                    enemyMovement = currentEnemy.GetComponent<Pathfinding2D>();
                    SearchForAllies();

                    if (currentEnemy.foundRival)
                    {
                        turnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + currentEnemy.transform.position, 0.1f, true);
                        yield return new WaitForSeconds(2f);
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
                    MoveInRange(enemyMovement);
                    
                    currentEnemy.foundRival = true;
                }
            }
            
            if (currentTarget != null && currentEnemy.passive == true)
            {
                //Do nothing.
                
                currentEnemy.foundRival = true;
            }
            
            if (currentTarget != null && currentEnemy.aggressive == true)
            {
                enemyMovement.FindPath(currentEnemy.transform.position, currentTarget.transform.position);
                MoveAggressive(enemyMovement);

                currentEnemy.foundRival = true;
            }
        }
        
        private void MoveInRange(Pathfinding2D unitPath)
        {
            foreach (var t in unitPath.path.Take(unitPath.path.Count - 1))
            {
                currentEnemy.transform.DOMove(t.worldPosition, 0.5f, true);
            }

            if (currentEnemy.hitPoints > 0 && currentPlayer.hitPoints > 0)
            {
                turnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + currentEnemy.transform.position, 0.1f, true);
                EnemyCombat();
            }
        }
        
        private void MoveAggressive(Pathfinding2D unitPath)
        {
            var count = 0;
            
            if (unitPath.path.Count <= currentEnemy.movement)
            {
                while (count < currentEnemy.movement - 1)
                {
                    currentEnemy.transform.DOMove(unitPath.path[count].worldPosition, 0.6f, true);
                
                    count++;
                }
                
                count = 0;
            }
            else
            {
                while (count < currentEnemy.movement)
                {
                    currentEnemy.transform.DOMove(unitPath.path[count].worldPosition, 0.6f, true);
                
                    count++;
                }
                
                count = 0;
            }

            if (currentEnemy.hitPoints > 0 && currentPlayer.hitPoints > 0)
            {
                turnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + currentEnemy.transform.position, 0.1f, true);
                //EnemyCombat();
            }
        }

        private void EnemyCombat()
        {
            currentTarget = null;
            
            if (currentPlayer != null)
            {
                StartCoroutine(currentEnemy.Attack(currentPlayer));
            }

            currentEnemy.foundRival = false;
        }
    }
}