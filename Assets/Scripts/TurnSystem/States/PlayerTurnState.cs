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

            yield return new WaitForSeconds(2f);
            
            TurnSystem.titleSystem.RemoveTitle(TurnSystem.playerTitle);
        }

        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(1f);
            
            // !! Puts all states in true !!

            for (int i = 0; i < TurnSystem.allyTeam.Count; i++)
            {
                TurnSystem.allyTeam[i].hasMoved = true;
                TurnSystem.allyTeam[i].hasAttacked = true;
            }
            
            // !! Checks if there are alive enemies, sends Enemy Turn state or Win state. !!

            for (int i = 0; i < TurnSystem.enemyTeam.Count; i++)
            {
                if (TurnSystem.enemyTeam[i].isDead != true)
                {
                    //TurnSystem.SetState(new EnemyTurnState(TurnSystem));
                    
                    Debug.Log("Enemy turn passed");
                    
                    TurnSystem.SetState(new PlayerTurnState(TurnSystem));
                }
            }
        }
    }
}
