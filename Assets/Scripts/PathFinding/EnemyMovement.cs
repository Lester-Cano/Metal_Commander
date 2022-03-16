using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;
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

                    yield return new WaitForSeconds(2f);
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
            }
        }
        
        void Move(Pathfinding2D unitPath)
        {
            currentEnemy.path.SetActive(false);
            foreach (var t in unitPath.path.Take(unitPath.path.Count - 1))
            {
                currentEnemy.transform.DOMove(t.worldPosition, 1f, true);
            }

            if (currentEnemy.hitPoints > 0 && currentPlayer.hitPoints > 0)
            {
                EnemyCombat();
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