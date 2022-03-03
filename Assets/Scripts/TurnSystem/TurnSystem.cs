using System;
using System.Collections.Generic;
using MapSystem;
using Menu___UI;
using TurnSystem.States;
using UnityEngine;

namespace TurnSystem
{
    public class TurnSystem : StateMachine
    {
        public List<Unit> allyTeam, enemyTeam;
        [SerializeField] public MapManager mapSystem;
        [SerializeField] public ButtonBehaviour screenSystem;
        [SerializeField] public TitleBehaviour titleSystem;
        
        [SerializeField] public GameObject playerTitle;
        [SerializeField] public GameObject enemyTitle;

        [SerializeField] public int enemyCount;
        [SerializeField] public int playerCount;

        private void Start()
        {
            // !! Initialize interface. !!
            SetState(new BeginBattleState(this));
        }

        private void Update()
        {
            if (playerCount >= allyTeam.Count)
            {
                SetState(new LostState(this));
            }

            if (enemyCount >= enemyTeam.Count)
            {
                SetState(new WonState(this));
            }
        }

        public void OnEndTurnButton()
        {
            StartCoroutine(State.CheckState());
        }
    }
}
