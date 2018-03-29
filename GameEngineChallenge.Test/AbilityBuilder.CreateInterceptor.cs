using System;
using System.Collections.Generic;

namespace GameEngineChallenge.Test
{
	public static partial class AbilityBuilder
	{
		public static IActionInterceptor CreateInterceptor( Func<IAction, GameContext, IEnumerable<IAction>> interceptor )
			=> new LambdaInterceptor( interceptor );

		private class LambdaInterceptor : IActionInterceptor
		{
			public LambdaInterceptor( Func<IAction, GameContext, IEnumerable<IAction>> interceptor )
			{
				_interceptor = interceptor;
				Id = new RequisiteId( Guid.NewGuid().ToString() );
			}

			private readonly Func<IAction, GameContext, IEnumerable<IAction>> _interceptor;
			public RequisiteId Id { get; }

			public IEnumerable<IAction> Intercept( IAction action, GameContext context ) => _interceptor( action, context );
		}
	}
}
