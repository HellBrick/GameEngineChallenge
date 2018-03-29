using GameEngineChallenge.Services;

namespace GameEngineChallenge
{
	public class GameContext
	{
		public GameContext( IHeroService heroService, IInputService inputService, ITimeService timeService, ISpaceService spaceService, IRandomService randomService )
		{
			HeroService = heroService;
			InputService = inputService;
			TimeService = timeService;
			SpaceService = spaceService;
			RandomService = randomService;
		}

		public IHeroService HeroService { get; }
		public IInputService InputService { get; }
		public ITimeService TimeService { get; }
		public ISpaceService SpaceService { get; }
		public IRandomService RandomService { get; }
	}
}
