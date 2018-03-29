using System;
using System.Collections.Generic;
using GameEngineChallenge.Actions;
using Utils;

namespace GameEngineChallenge.Abilities
{
	public class ReloadSlowdownAbility : IActionInterceptor
	{
		public ReloadSlowdownAbility( double multiplier )
			=> _multiplier = multiplier >= 0.0 ? multiplier : throw new ArgumentOutOfRangeException( "Multiplier can't be negative", nameof( multiplier ) );

		private readonly double _multiplier;
		public RequisiteId Id => new RequisiteId( nameof( ReloadSlowdownAbility ) );

		public IEnumerable<IAction> Intercept( IAction action, GameContext context )
		{
			return
				action is DecreaseTimerAction timerAction && IsAutoAttackCooldownTimer( timerAction.Timer )
				? new DecreaseTimerAction( timerAction.Timer, GetAdjustedDecrement() ).AsArray()
				: action.AsArray();

			bool IsAutoAttackCooldownTimer( ITimer timer )
				=> timer is CooldownAbility cooldownAbility
				&& cooldownAbility.InnerAbility is AutoAttackAbility;

			TimeSpan GetAdjustedDecrement() => TimeSpan.FromTicks( (long) ( timerAction.Decrement.Ticks * _multiplier ) );
		}
	}
}
