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
            
            Debug.Log("Player turn began");

            yield break;
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
                    TurnSystem.SetState(new PlayerTurnState(TurnSystem));
                }
                else
                {
                    TurnSystem.SetState(new WonState(TurnSystem));
                }
            }
        }
    }
}
