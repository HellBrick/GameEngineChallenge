using System;
using System.Collections.Generic;
using Utils;

namespace GameEngineChallenge.Test
{
	public static partial class AbilityBuilder
	{
		public static IActiveAbility CreateActiveAbility( TickPhase phase, Action<Hero, GameContext> actionLambda )
			=> CreateActiveAbility
			(
				phase,
				( hero, context ) => new LambdaAction( hero, actionLambda )
			);

		public static IActiveAbility CreateActiveAbility( TickPhase phase, Func<Hero, GameContext, IAction> actionFactory )
			=> CreateActiveAbility( phase, ( h, c ) => actionFactory( h, c ).AsArray() );

		public static IActiveAbility CreateActiveAbility( TickPhase phase, Func<Hero, GameContext, IReadOnlyCollection<IAction>> actionFactory )
			=> new LambdaActiveAbility( phase, actionFactory );

		private class LambdaAction : IAction
		{
			private readonly Hero _hero;
			private readonly Action<Hero, GameContext> _actionLambda;

			public LambdaAction( Hero hero, Action<Hero, GameContext> actionLambda )
				=> (_hero, _actionLambda) = (hero, actionLambda);

			public void Execute( GameContext context ) => _actionLambda( _hero, context );
		}

		private class LambdaActiveAbility : IActiveAbility
		{
			public LambdaActiveAbility( TickPhase phase, Func<Hero, GameContext, IReadOnlyCollection<IAction>> actionFactory )
			{
				Phase = phase;
				_actionFactory = actionFactory;
				Id = new RequisiteId( Guid.NewGuid().ToString() );
			}

			private readonly Func<Hero, GameContext, IReadOnlyCollection<IAction>> _actionFactory;

			public TickPhase Phase { get; }
			public RequisiteId Id { get; }

			public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context ) => _actionFactory( abilityOwner, context );
		}
	}
}
