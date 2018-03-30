using System;
using System.Collections.Generic;
using Utils;

namespace GameEngineChallenge.Test
{
	public static partial class RequisiteHelper
	{
		public static IActionInterceptor CreateInterceptor( Func<IAction, GameContext, OneOrMany<IAction>> interceptor )
			=> new LambdaInterceptor( interceptor );

		private class LambdaInterceptor : IActionInterceptor
		{
			public LambdaInterceptor( Func<IAction, GameContext, OneOrMany<IAction>> interceptor )
			{
				_interceptor = interceptor;
				Id = new RequisiteId( Guid.NewGuid().ToString() );
			}

			private readonly Func<IAction, GameContext, OneOrMany<IAction>> _interceptor;
			public RequisiteId Id { get; }

			public OneOrMany<IAction> Intercept( IAction action, GameContext context ) => _interceptor( action, context );
		}
	}
}
