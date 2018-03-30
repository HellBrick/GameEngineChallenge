using System;
using GameEngineChallenge.Actions;
using Utils;

namespace GameEngineChallenge.Abilities
{
	public class CriticalHitChanceAbility : IActionInterceptor
	{
		public CriticalHitChanceAbility( double chance, double damageMultiplier )
		{
			_chance = chance;
			_damageMultiplier = damageMultiplier >= 0.0 ? damageMultiplier : throw new ArgumentException( "Damage multiplier can't be negative", nameof( damageMultiplier ) );
		}

		private readonly double _chance;
		private readonly double _damageMultiplier;

		public RequisiteId Id => new RequisiteId( nameof( CriticalHitChanceAbility ) );

		public OneOrMany<IAction> Intercept( IAction action, GameContext context )
		{
			IAction result
				= action is DamageAction damageAction && context.RandomService.NextDouble() < _chance
				? new DamageAction( damageAction.Target, new HitPoints( (uint) ( Math.Round( damageAction.Damage.Value * _damageMultiplier ) ) ) )
				: action;

			return result.AsOne();
		}
	}
}
