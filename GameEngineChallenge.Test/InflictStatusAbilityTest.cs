using System;
using FluentAssertions;
using GameEngineChallenge.Abilities;
using GameEngineChallenge.Actions;
using GameEngineChallenge.Services;
using Utils;
using Xunit;

using static GameEngineChallenge.Test.RequisiteHelper;

namespace GameEngineChallenge.Test
{
	public class InflictStatusAbilityTest
	{
		[Fact]
		public void StatusIsNotInflictedIfTargetAlreadyHasIt()
		{
			IRequisite slowedDown = new MovementSlowdownAbility( 0.5 );
			Hero target = new Hero( new TeamId( 0 ), new HitPoints( 9999 ), slowedDown );
			IRequisite attack = new AutoAttackAbility( new HitPoints( 10 ), CreateTargeter( ( h, c ) => target ) );
			IRequisite inflictParalysis = new InflictStatusAbility( slowedDown, TimeSpan.FromSeconds( 2 ) );
			Hero attacker = new Hero( new TeamId( 1 ), initialHp: default, attack, inflictParalysis );

			HeroService heroService = new HeroService( new Hero[] { target, attacker } );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			new TickExecutor().ExecuteTick( context );

			target.Requisites.Count.Should().Be( 1 );
			target.HP.Should().Be( new HitPoints( 9989 ) );
		}

		[Fact]
		public void StatusIsInflictedIfTargetDoesNotHaveItAndThenDisappearsWhenTimeRunsOut()
		{
			IRequisite slowedDown = new MovementSlowdownAbility( 0.5 );
			Hero target = new Hero( new TeamId( 0 ), new HitPoints( 9999 ), new DecreaseAllTimersAbility() );

			IRequisite attack
				= new AutoAttackAbility( new HitPoints( 10 ), CreateTargeter( ( h, c ) => target ) )
				.WithCooldown( TimeSpan.FromSeconds( 42 ) );

			TimeSpan paralysisDuration = TimeSpan.FromSeconds( 2 );
			IRequisite inflictParalysis = new InflictStatusAbility( slowedDown, paralysisDuration );
			Hero attacker = new Hero( new TeamId( 1 ), initialHp: default, attack, inflictParalysis );

			HeroService heroService = new HeroService( new Hero[] { target, attacker } );
			TimeService timeService = new TimeService();
			GameContext context = new GameContext( heroService, new InputService(), timeService, new SpaceService(), new RandomService( new Random() ) );
			TickExecutor tickExecutor = new TickExecutor();

			tickExecutor.ExecuteTick( context );
			target.Requisites.Should().Contain( slowedDown );

			timeService.TimeElapsedSinceLastTick = paralysisDuration;
			tickExecutor.ExecuteTick( context );
			target.Requisites.Should().NotContain( slowedDown );
			target.Requisites.Count.Should().Be( 1 );
		}
	}
}

