using System;
namespace D2_RockPaperScissors
{
	public interface IHandGesture
	{
		public int GetPoints();
		public HandGesture GetHandGesture();
		public HandGesture WinsAgainst();
		public HandGesture LosesAagainst();
	}
}

