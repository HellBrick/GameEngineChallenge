using System.Collections.Generic;

namespace GameEngineChallenge.Services
{
	public interface IHeroService
	{
		IReadOnlyCollection<Hero> Heroes { get; }
	}

	public class HeroService : IHeroService
	{
		public HeroService( IReadOnlyCollection<Hero> heroes ) => Heroes = heroes;

		public IReadOnlyCollection<Hero> Heroes { get; }
	}
}
