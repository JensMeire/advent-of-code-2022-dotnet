using System;
namespace D2_RockPaperScissors
{
	public class Paper: IHandGesture
	{
        public HandGesture GetHandGesture()
        {
            return HandGesture.Paper;
        }

        public HandGesture LosesAagainst()
        {
            return HandGesture.Scissors;
        }

        public int GetPoints()
        {
            return 2;
        }

        public HandGesture WinsAgainst()
        {
            return HandGesture.Rock;
        }
    }
}

