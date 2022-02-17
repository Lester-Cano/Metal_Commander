using MapSystem;
using UnityEngine;

namespace TurnSystem
{
    public class TurnSystem : StateMachine
    {
        public Unit[] allyTeam, enemyTeam;
        [SerializeField] public MapManager mapSystem;

        private void Start()
        {
            // !! Initialize interface. !!
            SetState(new BeginBattleState(this));
        }

        public void OnEndTurnButton()
        {
            SetState(new PlayerTurnState(this));
        }
    }
}
