using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GameEngineChallenge.Abilities;
using GameEngineChallenge.Services;
using Utils;
using Xunit;

using static GameEngineChallenge.Test.AbilityBuilder;

namespace GameEngineChallenge.Test
{
	public class ReloadSlowdownAbilityTest
	{
		[Theory]
		[MemberData( nameof( EnumerateTestCases ) )]
		public void ReloadSlowdownMathChecksOut( IActiveAbility ability, TimeSpan expectedCooldown )
		{
			CooldownAbility autoAttack = ability.WithCooldown( TimeSpan.FromSeconds( 5.0 ) );

			Hero hero = new Hero( team: default, new HitPoints( 9999 ), autoAttack, new DecreaseAllTimersAbility(), new ReloadSlowdownAbility( 0.5 ) );
			HeroService heroService = new HeroService( hero.AsArray() );
			TimeService timeService = new TimeService();
			GameContext context = new GameContext( heroService, new InputService(), timeService, new SpaceService(), new RandomService( new Random() ) );
			TickExecutor tickExecutor = new TickExecutor();

			timeService.TimeElapsedSinceLastTick = TimeSpan.FromSeconds( 0.0 );
			tickExecutor.ExecuteTick( context );

			timeService.TimeElapsedSinceLastTick = TimeSpan.FromSeconds( 1.0 );
			tickExecutor.ExecuteTick( context );

			autoAttack.TimeLeft.Should().Be( expectedCooldown );
		}

		public static IEnumerable<object[]> EnumerateTestCases()
		{
			return EnumerateCaseData().Select( pair => new object[] { pair.Ability, pair.ExpectedCooldown } );

			IEnumerable<(IActiveAbility Ability, TimeSpan ExpectedCooldown)> EnumerateCaseData()
			{
				yield return (CreateActiveAbility( CommonTickPhases.OffensiveAbilities, ( h, c ) => { } ), TimeSpan.FromSeconds( 4.0 ));
				yield return (new AutoAttackAbility( new HitPoints( 10 ), CreateTargeter( ( h, _ ) => h ) ), TimeSpan.FromSeconds( 4.5 ));
			}
		}
	}
}
