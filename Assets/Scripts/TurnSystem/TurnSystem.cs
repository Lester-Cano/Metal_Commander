using System.Collections.Generic;
using MapSystem;
using TurnSystem.States;
using UnityEngine;

namespace TurnSystem
{
    public class TurnSystem : StateMachine
    {
        public List<Unit> allyTeam, enemyTeam;
        [SerializeField] public MapManager mapSystem;
        [SerializeField] public ButtonBehaviour screenSystem;

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
