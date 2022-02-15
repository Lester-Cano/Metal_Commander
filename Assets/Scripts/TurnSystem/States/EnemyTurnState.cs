using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : State
{
    public EnemyTurnState(TurnSystem turnSystem) : base(turnSystem)
    {
    }

    public override IEnumerator Start()
    {
        // !! Set "Enemy Turn" text. !!

        yield break;
    }

    public override IEnumerator Think()
    {
        // !! Set AI function. !!

        yield return new WaitForSeconds(2f);

        // !! Check if there are alive allies. !!

        // !! send a ally turn state or a lost state. !!
        TurnSystem.SetState(new LostState(TurnSystem));
        TurnSystem.SetState(new PlayerTurnState(TurnSystem));
    }
}
