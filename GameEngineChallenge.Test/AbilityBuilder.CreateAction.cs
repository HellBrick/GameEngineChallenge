using System;

namespace GameEngineChallenge.Test
{
	public static partial class AbilityBuilder
	{
		public static IAction CreateAction( Hero hero, Action<Hero, GameContext> actionLambda )
			=> new LambdaAction( hero, actionLambda );
	}
}
