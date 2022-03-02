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
            for (int j = 0; j < TurnSystem.enemyTeam.Count; j++)
            {
                TurnSystem.enemyTeam[j].hasMoved = false;
            }
                    
            for (int j = 0; j < TurnSystem.enemyTeam.Count; j++)
            {
                TurnSystem.enemyTeam[j].hasAttacked = false;
            }
            
            TurnSystem.titleSystem.SetTitle(TurnSystem.enemyTitle);

            yield return new WaitForSeconds(2f);
            
            TurnSystem.titleSystem.RemoveTitle(TurnSystem.enemyTitle);
        }

        public override IEnumerator Think()
        {
            yield return new WaitForSeconds(2f);
        }

        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(1f);
            
            for (int i = 0; i < TurnSystem.enemyTeam.Count; i++)
            {
                TurnSystem.enemyTeam[i].hasMoved = true;
                TurnSystem.enemyTeam[i].hasAttacked = true;
            }
            
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
