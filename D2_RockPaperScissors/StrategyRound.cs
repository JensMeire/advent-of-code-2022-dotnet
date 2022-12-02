using System;
namespace D2_RockPaperScissors
{
	public class StrategyRound
	{
        private readonly IHandGesture opponentGesture;
        private readonly RoundState state;

        public StrategyRound(IHandGesture opponentGesture, RoundState state)
		{
            this.opponentGesture = opponentGesture;
            this.state = state;
        }

        public int GetOutcomePoints()
        {
            return GetMatchOutcome() + GetPredictedHandGesture().GetPoints();
        }

        public int GetMatchOutcome()
        {
            return state switch
            {
                RoundState.Lose => 0,
                RoundState.Draw => 3,
                RoundState.Win => 6,
                _ => throw new NotImplementedException()
            };
        }

        public IHandGesture GetPredictedHandGesture()
        {
            if (state == RoundState.Draw)
                return CreateGesture(opponentGesture.GetHandGesture());

            if(state == RoundState.Win)
                return CreateGesture(opponentGesture.LosesAagainst());

            return CreateGesture(opponentGesture.WinsAgainst());
        }

        public IHandGesture CreateGesture(HandGesture gesture)
        {
            return gesture switch
            {
                HandGesture.Paper => new Paper(),
                HandGesture.Rock => new Rock(),
                HandGesture.Scissors => new Scissor(),
                _ => throw new NotImplementedException()
            };
        }
    }
}

