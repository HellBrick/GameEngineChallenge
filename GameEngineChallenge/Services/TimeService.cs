using System;

namespace GameEngineChallenge.Services
{
	public interface ITimeService
	{
		TimeSpan TimeElapsedSinceLastTick { get; }
	}

	public class TimeService : ITimeService
	{
		public TimeSpan TimeElapsedSinceLastTick { get; set; }
	}
}
