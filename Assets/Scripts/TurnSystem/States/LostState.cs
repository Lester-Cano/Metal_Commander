using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostState : State
{
    public LostState(TurnSystem turnSystem) : base(turnSystem)
    {
    }

    public override IEnumerator Start()
    {
        // !! Set Lost Screen. !!

        yield break;
    }
}
