using GameEngineChallenge.Services;

namespace GameEngineChallenge
{
	public class GameContext
	{
		public GameContext( IHeroService heroService )
		{
			HeroService = heroService;
		}

		public IHeroService HeroService { get; }
	}
}
