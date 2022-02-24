using System.Collections;

namespace TurnSystem.States
{
    public class LostState : State
    {
        public LostState(global::TurnSystem.TurnSystem turnSystem) : base(turnSystem)
        {
        }

        public override IEnumerator Start()
        {
            TurnSystem.screenSystem.LoadScene("Lost");

            yield break;
        }
    }
}
