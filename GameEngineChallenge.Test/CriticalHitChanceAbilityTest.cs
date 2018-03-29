using FluentAssertions;
using GameEngineChallenge.Abilities;
using GameEngineChallenge.Services;
using Xunit;

using static GameEngineChallenge.Test.AbilityBuilder;

namespace GameEngineChallenge.Test
{
	public class CriticalHitChanceAbilityTest
	{
		[Theory]
		[InlineData( 10, 1.35, 0.2, 0.2, 10 )]
		[InlineData( 10, 1.35, 0.2, 0.19, 14 )]
		public void DamageMathChecksOut( uint normalDamage, double damageMultiplier, double chance, double randomRoll, uint expectedDamage )
		{
			HitPoints initialHp = new HitPoints( 9999 );
			Hero shootingTarget = new Hero( new TeamId( 255 ), initialHp );

			HitPoints baseDamage = new HitPoints( normalDamage );
			AutoAttackAbility attackAbility = new AutoAttackAbility( baseDamage, CreateTargeter( ( h, c ) => shootingTarget ) );
			CriticalHitChanceAbility criticalHitChanceAbility = new CriticalHitChanceAbility( chance, damageMultiplier );

			Hero attacker = new Hero( new TeamId( 0 ), initialHp: default, attackAbility, criticalHitChanceAbility );

			HeroService heroService = new HeroService( new[] { shootingTarget, attacker } );
			RandomServiceMock randomService = new RandomServiceMock();
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), randomService );

			randomService.NextRoll = randomRoll;
			new TickExecutor().ExecuteTick( context );

			( initialHp.Value - shootingTarget.HP.Value ).Should().Be( expectedDamage );
		}

		private class RandomServiceMock : IRandomService
		{
			public double NextRoll { get; set; }
			public double NextDouble() => NextRoll;
		}
	}
}
