using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : State
{
    public PlayerTurnState(TurnSystem.TurnSystem turnSystem) : base(turnSystem)
    {
    }

    public override IEnumerator Start()
    {
        // !! Set "Player Turn" text. !!

        Debug.Log("its playerss turnn");

        yield break;
    }

    public override IEnumerator CheckState()
    {
        yield return new WaitForSeconds(2f);
        

        // !! Check if there are alive enemies. !!

        // !! send a win state !!
        //TurnSystem.SetState(new WonState(TurnSystem));
    }
}
