using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonState : State
{
    public WonState(TurnSystem.TurnSystem turnSystem) : base(turnSystem)
    {
    }

    public override IEnumerator Start()
    {
        // !! Set Won Screen. !!

        yield break;
    }
}
