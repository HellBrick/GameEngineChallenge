using System;
using GameEngineChallenge.Actions;
using Utils;

namespace GameEngineChallenge.Abilities
{
	public class MovementSlowdownAbility : IActionInterceptor
	{
		public MovementSlowdownAbility( double multiplier )
			=> _multiplier = multiplier >= 0.0 ? multiplier : throw new ArgumentOutOfRangeException( "Multiplier can't be negative", nameof( multiplier ) );

		private readonly double _multiplier;

		public RequisiteId Id => new RequisiteId( nameof( MovementSlowdownAbility ) );

		public OneOrMany<IAction> Intercept( IAction action, GameContext context )
			=> action is MoveAction moveAction
			? new MoveAction( moveAction.Hero, moveAction.MoveVector * _multiplier ).AsOne<IAction>()
			: action.AsOne();
	}
}
