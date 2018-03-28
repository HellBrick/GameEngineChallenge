using System.Collections.Generic;
using System.Linq;
using Utils.SpaceTime;

namespace GameEngineChallenge.Abilities.AttackTargeters
{
	public class ClosestOpponentAttackTargeter : IAttackTargeter
	{
		public ClosestOpponentAttackTargeter( Distance radius ) => _radius = radius;

		private readonly Distance _radius;

		public IEnumerable<Hero> EnumerateTargets( Hero attacker, GameContext context )
		{
			Position attackerPosition = context.SpaceService.GetHeroPosition( attacker );

			return
				context.HeroService.Heroes
				.Where( hero => hero.Team != attacker.Team && hero.HP > new HitPoints( 0 ) )
				.Select( hero => (Hero: hero, Distance: Distance.Between( attackerPosition, context.SpaceService.GetHeroPosition( hero ) )) )
				.Where( pair => pair.Distance <= _radius )
				.OrderBy( pair => pair.Distance )
				.Select( pair => pair.Hero )
				.Take( 1 );
		}
	}
}
