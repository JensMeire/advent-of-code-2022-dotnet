using System;
namespace D2_RockPaperScissors
{
	public class Round
	{
        public IHandGesture OpponentsGesture { get; }
        public IHandGesture YourGesture { get; }

        public Round(IHandGesture opponentsGesture, IHandGesture yourGesture)
		{
            OpponentsGesture = opponentsGesture;
            YourGesture = yourGesture;
        }

        public int GetOutcomePoints()
        {
            return GetMatchOutcome() + YourGesture.GetPoints();
        }

        public int GetMatchOutcome()
        {
            if (YourGesture.GetHandGesture() == OpponentsGesture.GetHandGesture())
                return 3;

            if (OpponentsGesture.LosesAagainst() == YourGesture.GetHandGesture())
                return 6;

             return 0;
        }
    }
}

