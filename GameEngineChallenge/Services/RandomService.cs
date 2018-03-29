using System;

namespace GameEngineChallenge.Services
{
	public interface IRandomService
	{
		double NextDouble();
	}

	public class RandomService : IRandomService
	{
		public RandomService( Random random ) => _random = random;

		private readonly Random _random;

		public double NextDouble() => _random.NextDouble();
	}
}
