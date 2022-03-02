using System.Collections;
using UnityEngine;

namespace TurnSystem.States
{
    public class BeginBattleState : State
    {
        public BeginBattleState(global::TurnSystem.TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override IEnumerator Start()
        {
            // !! Set "tittle level" text. !!
        
            TurnSystem.mapSystem.SpawnUnit();
            TurnSystem.mapSystem.SpawnEnemies();
            

            yield return new WaitForSeconds(0.2f);

            TurnSystem.SetState(new PlayerTurnState(TurnSystem));
        }
    }
}
