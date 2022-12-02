using System;
namespace D2_RockPaperScissors
{
	public static class Parser
	{
		public static List<Round> Parse(string filePath)
		{
			return File.ReadLines(filePath).Select(line => new Round(GetGesture(line[0]), GetGesture(line[2]))).ToList();
		}

        public static List<StrategyRound> Parse2(string filePath)
        {
            return File.ReadLines(filePath).Select(line => new StrategyRound(GetGesture(line[0]), GetState(line[2]))).ToList();
        }

        private static IHandGesture GetGesture(char val)
		{
			switch (val) {
				case 'A':
				case 'X': return new Rock();
				case 'B':
				case 'Y': return new Paper();
				case 'C':
				case 'Z': return new Scissor();
				default: throw new NotImplementedException();
			}
		}

        private static RoundState GetState(char val)
        {
            switch (val)
            {
                case 'X': return RoundState.Lose;
                case 'Y': return RoundState.Draw;
                case 'Z': return RoundState.Win;
                default: throw new NotImplementedException();
            }
        }
    }
}

