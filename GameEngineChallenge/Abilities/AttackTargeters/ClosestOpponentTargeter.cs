﻿using System.Collections.Generic;
using System.Linq;
using Utils.SpaceTime;

namespace GameEngineChallenge.Abilities.AttackTargeters
{
	public class ClosestOpponentTargeter : ITargeter
	{
		public ClosestOpponentTargeter( Distance radius ) => _radius = radius;

		private readonly Distance _radius;

		public IEnumerable<Hero> EnumerateTargets( Hero actor, GameContext context )
		{
			Position attackerPosition = context.SpaceService.GetHeroPosition( actor );

			return
				context.HeroService.Heroes
				.Where( hero => hero.Team != actor.Team && hero.HP > HitPoints.Zero )
				.Select( hero => (Hero: hero, Distance: Distance.Between( attackerPosition, context.SpaceService.GetHeroPosition( hero ) )) )
				.Where( pair => pair.Distance <= _radius )
				.OrderBy( pair => pair.Distance )
				.Select( pair => pair.Hero )
				.Take( 1 );
		}
	}
}
