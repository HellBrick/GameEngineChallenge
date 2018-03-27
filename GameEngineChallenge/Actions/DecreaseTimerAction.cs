using System;

namespace GameEngineChallenge.Actions
{
	public class DecreaseTimerAction : IAction
	{
		public DecreaseTimerAction( ITimer timer, TimeSpan decrement )
		{
			Timer = timer;
			Decrement = decrement;
		}

		public ITimer Timer { get; }
		public TimeSpan Decrement { get; }

		public void Execute( GameContext context )
		{
			Timer.TimeLeft -= Decrement;
			if ( Timer.TimeLeft < TimeSpan.Zero )
				Timer.TimeLeft = TimeSpan.Zero;
		}
	}
}
