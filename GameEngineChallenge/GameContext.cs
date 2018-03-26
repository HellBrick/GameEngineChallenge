using GameEngineChallenge.Services;

namespace GameEngineChallenge
{
	public class GameContext
	{
		public GameContext( IHeroService heroService, IInputService inputService, ITimeService timeService, ISpaceService spaceService )
		{
			HeroService = heroService;
			InputService = inputService;
			TimeService = timeService;
			SpaceService = spaceService;
		}

		public IHeroService HeroService { get; }
		public IInputService InputService { get; }
		public ITimeService TimeService { get; }
		public ISpaceService SpaceService { get; }
	}
}
