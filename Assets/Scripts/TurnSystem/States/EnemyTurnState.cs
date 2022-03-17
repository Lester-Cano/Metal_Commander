using System.Collections;
using UnityEngine;
using PathFinding;
using DG.Tweening;

namespace TurnSystem.States
{
    public class EnemyTurnState : State
    {

        [SerializeField] public EnemyMovement enemyMovement;

        public EnemyTurnState(global::TurnSystem.TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override IEnumerator Start()
        {
            TurnSystem.titleSystem.SetTitle(TurnSystem.enemyTitle);
            TurnSystem.playerUI.SetActive(false);

            yield return new WaitForSeconds(1f);
            
            TurnSystem.titleSystem.RemoveTitle(TurnSystem.enemyTitle);
            
            bool cameraSet = false;

            // foreach (var unit in TurnSystem.enemyTeam)
            // {
            //     if (unit.hitPoints > 0)
            //     {
            //         TurnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + unit.transform.position, 0.5f, false);
            //         cameraSet = true;
            //     }
            // }
            //
            // cameraSet = false;

            enemyMovement = TurnSystem.enemyMovement;
            enemyMovement.StartCombat = true;
        }
        
        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(0.5f);
            
            for (int i = 0; i < TurnSystem.allyTeam.Count; i++)
            {
                if (TurnSystem.allyTeam[i].isDead != true)
                {
                    TurnSystem.SetState(new PlayerTurnState(TurnSystem));
                }
            }
        }
    }
}
