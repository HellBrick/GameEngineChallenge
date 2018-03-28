﻿using System.Collections.Generic;
using System.Linq;
using Utils;

namespace GameEngineChallenge
{
	public class TickExecutor
	{
		public void ExecuteTick( GameContext context )
		{
			IReadOnlyCollection<Hero> heroes = context.HeroService.Heroes;

			TickPhase currentPhaseCandidate = new TickPhase( 0 );
			while ( TryGetPhaseGreaterThanOrEqualTo( currentPhaseCandidate ) is TickPhase currentPhase )
			{
				ExecutePhase( currentPhase );
				currentPhaseCandidate = new TickPhase( currentPhase.Order + 1 );
			}

			TickPhase? TryGetPhaseGreaterThanOrEqualTo( TickPhase minAllowedPhase )
				=> heroes
				.SelectMany( hero => hero.Requisites )
				.OfType<IActiveAbility>()
				.Select( ability => ability.Phase )
				.Where( phase => phase >= minAllowedPhase )
				.MinOrNull();

			void ExecutePhase( TickPhase currentPhase )
				=> heroes
				.SelectMany( hero => hero.Requisites.OfType<IActiveAbility>(), ( hero, ability ) => (hero, ability) )
				.Where( pair => pair.ability.Phase == currentPhase )
				.SelectMany( pair => pair.ability.GetActions( pair.hero, context ) )
				.ToArray()
				.ForEach( context, ( capturedContext, action ) => action.Execute( capturedContext ) );
		}
	}
}