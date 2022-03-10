using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
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
        [SerializeField] Unit currentEnemy;
        [SerializeField] GameObject currentTarget = null;
        [SerializeField] private Tilemap map;

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

                enemyMovement = currentEnemy.GetComponent<Pathfinding2D>();
                SearchForAllies();

                yield return new WaitForSeconds(2f);

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
            float distanciaMinima = 4f;
;

            for (int i = 0; i < allies.Count; i++)
            {
                float distanciaMinimaActual = Vector3.Distance(currentEnemy.transform.position, allies[i].transform.position);

                if (distanciaMinimaActual <= distanciaMinima)
                {
                    distanciaMinima = distanciaMinimaActual;
                    currentTarget = allies[i].gameObject;
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
            EnemyCombat();
        }

        void EnemyCombat()
        {
            Unit currentUnit = currentEnemy.GetComponent<Unit>();
            Unit currentTargetUnit = currentTarget.GetComponent<Unit>();
            currentTarget = null;

            if (currentUnit == null || currentTargetUnit == null)
            {
                return;
            }
            else
            {
                

                if (currentTargetUnit.hitPoints > 0)
                {
                    currentUnit.Attack(currentTargetUnit);
                    currentTargetUnit.Attack(currentUnit);
                }
                else
                {
                    currentTargetUnit.isDead = true;
                    turnSystem.playerCount++;
                }

               
            }

        }

    }
}