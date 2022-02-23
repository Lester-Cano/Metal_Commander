using System.Collections;
using UnityEngine;

namespace TurnSystem.States
{
    public class EnemyTurnState : State
    {
        public EnemyTurnState(global::TurnSystem.TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override IEnumerator Start()
        {
            // !! Set "Enemy Turn" text. !!
            
            Debug.Log("Enemy turn began");

            yield break;
        }

        public override IEnumerator Think()
        {
            // !! Set AI function. !!

            yield return new WaitForSeconds(2f);
        }

        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(1f);
            
            // !! Checks if there are alive allies, sends Player Turn state or Lost state. !!
            
            for (int i = 0; i < TurnSystem.allyTeam.Count; i++)
            {
                if (TurnSystem.allyTeam[i].isDead != true)
                {
                    TurnSystem.SetState(new PlayerTurnState(TurnSystem));
                }
                else
                {
                    TurnSystem.SetState(new LostState(TurnSystem));
                }
            }
        }
    }
}
