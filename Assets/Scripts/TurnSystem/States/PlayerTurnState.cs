using System.Collections;
using UnityEngine;

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
