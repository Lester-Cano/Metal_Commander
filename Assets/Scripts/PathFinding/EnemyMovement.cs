using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TurnSystem.States;

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

        [SerializeField] public bool StartCombat = false;

        [SerializeField] private bool rivalFound;
        

        void Start()
        {
            FindEntities();
        }

        void Update()
        {
            if (StartCombat)
            {
                StartCoroutine(StartEnemy());
            }
        }
        
        IEnumerator StartEnemy()
        {
            StartCombat = false;
            
            for (int i = 0; i < enemies.Count; i++)
            {
                currentEnemy = enemies[i];

                if (currentEnemy.hitPoints > 0)
                {
                    enemyMovement = currentEnemy.GetComponent<Pathfinding2D>();
                    SearchForAllies();

                    if (rivalFound)
                    {
                        turnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + currentEnemy.transform.position, 0.1f, true);
                        yield return new WaitForSeconds(2f);
                    }

                    yield return new WaitForSeconds(0.1f);
                }
            }
            
            turnSystem.SetState(new PlayerTurnState(turnSystem));
        }
        
        void FindEntities()
        {
            enemies = turnSystem.enemyTeam;
            allies = turnSystem.allyTeam;
        }
        
        void SearchForAllies()
        {
            var minDistance = 4f;
;
            foreach (var t in allies)
            {
                var actualMinDistance = Vector3.Distance(currentEnemy.transform.position, t.transform.position);

                if (actualMinDistance <= minDistance && t.hitPoints > 0)
                {
                    minDistance = actualMinDistance;
                    currentTarget = t.gameObject;
                    currentPlayer = currentTarget.GetComponent<Unit>();
                }
            }
            if (currentTarget != null)
            {
                enemyMovement.FindPath(currentEnemy.transform.position, currentTarget.transform.position);
                Move(enemyMovement);

                rivalFound = true;
            }
        }
        
        void Move(Pathfinding2D unitPath)
        {
            foreach (var t in unitPath.path.Take(unitPath.path.Count - 1))
            {
                currentEnemy.transform.DOMove(t.worldPosition, 0.5f, true);
            }

            if (currentEnemy.hitPoints > 0 && currentPlayer.hitPoints > 0)
            {
                turnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + currentEnemy.transform.position, 0.1f, true);
                EnemyCombat();

                rivalFound = false;
            }
        }

        void EnemyCombat()
        {
            currentTarget = null;
            
            if (currentPlayer != null)
            {
                StartCoroutine(currentEnemy.Attack(currentPlayer));
            }
        }
    }
}