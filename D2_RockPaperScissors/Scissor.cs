namespace D2_RockPaperScissors
{
	public class Scissor: IHandGesture
	{
        public HandGesture GetHandGesture()
        {
            return HandGesture.Scissors;
        }

        public HandGesture LosesAagainst()
        {
            return HandGesture.Rock;
        }

        public int GetPoints()
        {
            return 3;
        }

        public HandGesture WinsAgainst()
        {
            return HandGesture.Paper;
        }
    }
}

