using System.Collections;
using UnityEngine;
using PathFinding;

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

            yield return new WaitForSeconds(2f);
            
            TurnSystem.titleSystem.RemoveTitle(TurnSystem.enemyTitle);

            this.enemyMovement = TurnSystem.enemyMovement;
            enemyMovement.StartCombat = true;

            
        }
        
        public override IEnumerator CheckState()
        {
            yield return new WaitForSeconds(1f);
            
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
