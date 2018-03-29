using System;
using System.Collections.Generic;
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

		public IEnumerable<IAction> Intercept( IAction action, GameContext context )
			=> action is MoveAction moveAction
			? new MoveAction( moveAction.Hero, moveAction.MoveVector * _multiplier ).AsArray()
			: action.AsArray();
	}
}
