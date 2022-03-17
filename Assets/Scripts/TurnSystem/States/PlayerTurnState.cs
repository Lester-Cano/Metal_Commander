using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace TurnSystem.States
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(global::TurnSystem.TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override IEnumerator Start()
        {
            // !! Reset values. !!
            
            for (int j = 0; j < TurnSystem.allyTeam.Count; j++)
            {
                TurnSystem.allyTeam[j].hasMoved = false;
            }
                    
            for (int j = 0; j < TurnSystem.allyTeam.Count; j++)
            {
                TurnSystem.allyTeam[j].hasAttacked = false;
            }
            
            // !! Set "Player Turn" text. !!

            TurnSystem.titleSystem.SetTitle(TurnSystem.playerTitle);
            TurnSystem.playerUI.SetActive(true);

            yield return new WaitForSeconds(1f);
            
            TurnSystem.titleSystem.RemoveTitle(TurnSystem.playerTitle);

            bool cameraSet = false;

            foreach (var unit in TurnSystem.allyTeam)
            {
                if (unit.hitPoints > 0)
                {
                    TurnSystem.mainCamera.transform.DOMove(new Vector3(0, 0, -10) + unit.transform.position, 0.5f, false);
                    //TurnSystem.cameraController.ReBound();
                    
                    cameraSet = true;
                }
            }

            cameraSet = false;
        }

        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < TurnSystem.allyTeam.Count; i++)
            {
                TurnSystem.allyTeam[i].hasMoved = true;
                TurnSystem.allyTeam[i].hasAttacked = true;
            }

            for (int i = 0; i < TurnSystem.enemyTeam.Count; i++)
            {
                if (TurnSystem.enemyTeam[i].isDead != true)
                {
                    TurnSystem.SetState(new EnemyTurnState(TurnSystem));
                }
            }
        }
    }
}
