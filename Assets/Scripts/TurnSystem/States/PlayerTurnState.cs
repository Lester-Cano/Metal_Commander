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
            // !! Set "Player Turn" text. !!

            TurnSystem.titleSystem.SetTitle(TurnSystem.playerTitle);

            yield return new WaitForSeconds(2f);
            
            TurnSystem.titleSystem.RemoveTitle(TurnSystem.playerTitle);
        }

        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(1f);
            
            // !! Checks if there are alive enemies, sends Enemy Turn state or Win state. !!

            for (int i = 0; i < TurnSystem.enemyTeam.Count; i++)
            {
                if (TurnSystem.enemyTeam[i].isDead != true)
                {
                    //Uncheck when AI ready.
                    //TurnSystem.SetState(new EnemyTurnState(TurnSystem));
                    
                    Debug.Log("Enemy turn passed");

                    for (int j = 0; j < TurnSystem.enemyTeam.Count; j++)
                    {
                        TurnSystem.allyTeam[i].hasMoved = false;
                    }

                    
                }
                else
                {
                    TurnSystem.SetState(new WonState(TurnSystem));
                }
            }
        }
    }
}
