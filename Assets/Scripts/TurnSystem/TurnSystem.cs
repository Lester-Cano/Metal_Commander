using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : StateMachine
{
    public Unit[] allyTeam, enemyteam;
    public Unit unit;
    [SerializeField] public Map map;
    public Sprite sprite;

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
