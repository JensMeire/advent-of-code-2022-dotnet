using System;
namespace D2_RockPaperScissors
{
	public class Rock: IHandGesture
	{

        public HandGesture GetHandGesture()
        {
            return HandGesture.Rock;
        }

        public int GetPoints()
        {
            return 1;
        }

        public HandGesture WinsAgainst()
        {
            return HandGesture.Scissors;
        }

        public HandGesture LosesAagainst()
        {
            return HandGesture.Paper;
        }
    }
}

