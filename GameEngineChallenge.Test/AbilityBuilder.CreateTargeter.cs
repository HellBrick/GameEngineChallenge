using System;
using System.Collections.Generic;
using GameEngineChallenge.Abilities;
using Utils;

namespace GameEngineChallenge.Test
{
	public static partial class AbilityBuilder
	{
		public static ITargeter CreateTargeter( Func<Hero, GameContext, Hero> targetSelector )
			=> new LambdaTargeter( ( h, c ) => targetSelector( h, c ).AsArray() );

		public static ITargeter CreateTargeter( Func<Hero, GameContext, IEnumerable<Hero>> targetSelector )
			=> new LambdaTargeter( targetSelector );

		private class LambdaTargeter : ITargeter
		{
			public LambdaTargeter( Func<Hero, GameContext, IEnumerable<Hero>> targetSelector ) => _targetSelector = targetSelector;

			private readonly Func<Hero, GameContext, IEnumerable<Hero>> _targetSelector;

			public IEnumerable<Hero> EnumerateTargets( Hero actor, GameContext context ) => _targetSelector( actor, context );
		}
	}
}
