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

            yield break;
        }

        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(2f);
        

            // !! Checks if there are alive enemies, sends Enemy Turn state or Win state. !!

            for (int i = 0; i < TurnSystem.enemyTeam.Count; i++)
            {
                if (TurnSystem.enemyTeam[i].isDead != true)
                {
                    TurnSystem.SetState(new EnemyTurnState(TurnSystem));
                }
                else
                {
                    TurnSystem.SetState(new WonState(TurnSystem));
                }
            }
        }
    }
}
