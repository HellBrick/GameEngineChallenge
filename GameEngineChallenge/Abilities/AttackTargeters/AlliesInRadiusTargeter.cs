using System.Collections.Generic;
using System.Linq;
using Utils.SpaceTime;

namespace GameEngineChallenge.Abilities.AttackTargeters
{
	public class AlliesInRadiusTargeter : ITargeter
	{
		public AlliesInRadiusTargeter( Distance radius ) => _radius = radius;

		private readonly Distance _radius;

		public IEnumerable<Hero> EnumerateTargets( Hero actor, GameContext context )
		{
			Position actorPosition = context.SpaceService.GetHeroPosition( actor );
			return
				context.HeroService.Heroes
				.Where( hero => hero.Team == actor.Team && hero != actor )
				.Select( hero => (Hero: hero, Distance: Distance.Between( actorPosition, context.SpaceService.GetHeroPosition( hero ) )) )
				.Where( pair => pair.Distance <= _radius )
				.Select( pair => pair.Hero );
		}
	}
}
