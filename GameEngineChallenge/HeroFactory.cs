using System;
using GameEngineChallenge.Abilities;
using GameEngineChallenge.Abilities.AttackTargeters;
using Utils.SpaceTime;

namespace GameEngineChallenge
{
	public static class HeroFactory
	{
		public static Hero CreateHero1( TeamId team )
			=> new Hero
			(
				team,
				new HitPoints( 100 ),

				new InputBasedMovementAbility( new Speed( 2.0 ) ),
				new AutoAttackAbility( new HitPoints( 5 ), new ClosestOpponentTargeter( new Distance( 2.0 ) ) ).WithCooldown( TimeSpan.FromSeconds( 1.5 ) ),
				new InflictStatusAbility( new ReloadSlowdownAbility( 0.5 ), TimeSpan.FromSeconds( 1 ) ),
				DeathOnNoHpAbility.Instance,
				DecreaseAllTimersAbility.Instance
			);

		public static Hero CreateHero2( TeamId team )
			=> new Hero
			(
				team,
				new HitPoints( 100 ),

				new InputBasedMovementAbility( new Speed( 2.0 ) ),
				new AutoAttackAbility( new HitPoints( 2 ), new ClosestOpponentTargeter( new Distance( 10.0 ) ) ).WithCooldown( TimeSpan.FromSeconds( 1.5 ) ),
				new InflictStatusAbility( new MovementSlowdownAbility( 0.5 ), TimeSpan.FromSeconds( 1 ) ),
				DeathOnNoHpAbility.Instance,
				DecreaseAllTimersAbility.Instance
			);

		public static Hero CreateHero3( TeamId team )
			=> new Hero
			(
				team,
				new HitPoints( 100 ),

				new InputBasedMovementAbility( new Speed( 2.0 ) ),
				new AutoAttackAbility( new HitPoints( 2 ), new ClosestOpponentTargeter( new Distance( 2.0 ) ) ).WithCooldown( TimeSpan.FromSeconds( 1.5 ) ),
				new AuraAbility( new CriticalHitChanceAbility( chance: 0.5, damageMultiplier: 2.0 ), new AlliesInRadiusTargeter( new Distance( 5.0 ) ) ),
				DeathOnNoHpAbility.Instance,
				DecreaseAllTimersAbility.Instance
			);
	}
}
