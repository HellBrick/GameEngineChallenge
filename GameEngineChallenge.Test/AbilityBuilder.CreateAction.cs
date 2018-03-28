using System;

namespace GameEngineChallenge.Test
{
	public static partial class AbilityBuilder
	{
		public static IAction CreateAction( Action<GameContext> actionLambda )
			=> new LambdaAction( default, ( _, c ) => actionLambda( c ) );

		public static IAction CreateAction( Hero hero, Action<Hero, GameContext> actionLambda )
			=> new LambdaAction( hero, actionLambda );
	}
}
