using System;
using FluentAssertions;
using GameEngineChallenge.Abilities;
using GameEngineChallenge.Abilities.AttackTargeters;
using GameEngineChallenge.Actions;
using GameEngineChallenge.Services;
using Utils;
using Utils.SpaceTime;
using Xunit;

using static GameEngineChallenge.Test.AbilityBuilder;

namespace GameEngineChallenge.Test
{
	public class DeathTest
	{
		[Fact]
		public void UnitDiesWhenHpGetDownToZero()
		{
			IActiveAbility suicideAbility = CreateActiveAbility( CommonTickPhases.OffensiveAbilities, ( hero, _ ) => new DamageAction( hero, hero.HP ) );

			Hero dyingHero = new Hero( team: default, new HitPoints( 10 ), DeathOnNoHpAbility.Instance, suicideAbility );
			HeroService heroService = new HeroService( dyingHero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService() );

			TickExecutor tickExecutor = new TickExecutor();

			tickExecutor.ExecuteTick( context );
			dyingHero.HP.Should().Be( HitPoints.Zero );

			tickExecutor.ExecuteTick( context );
			dyingHero.Requisites.Should().Contain( DeadAbility.Instance );
		}

		[Fact]
		public void DeadUnitCantMoveOrAttack()
		{
			Hero deadHero = new Hero
			(
				new TeamId( 0 ),
				HitPoints.Zero,

				DeadAbility.Instance,
				new InputBasedMovementAbility( new Speed( 5.0 ) ),
				new AutoAttackAbility( new HitPoints( 5 ), new ClosestOpponentTargeter( new Distance( 9999.0 ) ) ).WithCooldown( TimeSpan.FromSeconds( 5 ) )
			);

			HitPoints shootingTargetHp = new HitPoints( 9999 );
			Hero shootingTarget = new Hero( new TeamId( 1 ), shootingTargetHp );

			HeroService heroService = new HeroService( new[] { deadHero, shootingTarget } );

			SpaceService spaceService = new SpaceService();
			Position bodyPosition = new Position( 0.0, 0.0 );
			spaceService.SetHeroPosition( deadHero, bodyPosition );
			spaceService.SetHeroPosition( shootingTarget, bodyPosition + new Vector( 0.5, -1.2 ) );

			TimeService timeService = new TimeService();
			timeService.TimeElapsedSinceLastTick = TimeSpan.FromSeconds( 1 );

			InputService inputService = new InputService();
			inputService.SetDirection( deadHero, new Vector( 1.0, 0.0 ) );

			GameContext context = new GameContext( heroService, inputService, timeService, spaceService );
			new TickExecutor().ExecuteTick( context );

			spaceService.GetHeroPosition( deadHero ).Should().Be( bodyPosition );
			shootingTarget.HP.Should().Be( shootingTargetHp );
		}
	}
}
