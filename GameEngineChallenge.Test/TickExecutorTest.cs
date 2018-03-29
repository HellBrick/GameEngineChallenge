using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GameEngineChallenge.Actions;
using GameEngineChallenge.Services;
using Utils;
using Xunit;

using static GameEngineChallenge.Test.AbilityBuilder;

namespace GameEngineChallenge.Test
{
	public class TickExecutorTest
	{
		[Fact]
		public void TickPhasesAreExecutedInCorrectOrder()
		{
			List<TickPhase> executionOrder = new List<TickPhase>();
			TickPhase phase0 = new TickPhase( 0 );
			TickPhase phase1 = new TickPhase( 1 );
			TickPhase phase4 = new TickPhase( 4 );

			Hero hero = new Hero( team: default, initialHp: default, new IRequisite[] { CreatePhaseAbility( phase1 ), CreatePhaseAbility( phase4 ), CreatePhaseAbility( phase0 ) } );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			executionOrder.Should().HaveElementAt( 0, phase0 );
			executionOrder.Should().HaveElementAt( 1, phase1 );
			executionOrder.Should().HaveElementAt( 2, phase4 );

			IActiveAbility CreatePhaseAbility( TickPhase phase ) => CreateActiveAbility( phase, ( h, c ) => executionOrder.Add( phase ) );
		}

		[Theory]
		[InlineData( 1, 0, false )]
		[InlineData( 0, 0, false )]
		[InlineData( 0, 1, true )]
		[InlineData( 0, 13, true )]
		public void AbilityAddedIntoCurrentPhaseIsNotExecuted( uint inflicterPhase, uint newAbilityPhase, bool shouldExecuteNewAbility )
		{
			bool newAbilityExecuted = false;

			IActiveAbility newAbility = CreateActiveAbility( new TickPhase( newAbilityPhase ), ( h, c ) => newAbilityExecuted = true );
			IActiveAbility newAbilityInflicter = CreateActiveAbility( new TickPhase( inflicterPhase ), ( h, c ) => new AddRequisiteAction( h, newAbility ) );

			Hero hero = new Hero( team: default, initialHp: default, newAbilityInflicter );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			newAbilityExecuted.Should().Be( shouldExecuteNewAbility );
		}

		[Fact]
		public void RemovedAbilityIsNotExecuted()
		{
			bool removedAbilityExecuted = false;

			IActiveAbility removedAbility = CreateActiveAbility( new TickPhase( 1 ), ( h, c ) => removedAbilityExecuted = true );
			IActiveAbility abilityRemover = CreateActiveAbility( new TickPhase( 0 ), ( h, c ) => new RemoveRequisiteAction( h, removedAbility ) );

			Hero hero = new Hero( team: default, initialHp: default, removedAbility, abilityRemover );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			removedAbilityExecuted.Should().Be( false );
		}

		[Fact]
		public void InterceptorCanDisableAction()
		{
			bool actionExecuted = false;

			IActiveAbility ability = CreateActiveAbility( phase: default, ( h, c ) => actionExecuted = true );
			IActionInterceptor interceptor = CreateInterceptor( ( a, c ) => OneOrMany<IAction>.Empty );

			Hero hero = new Hero( team: default, initialHp: default, ability, interceptor );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			actionExecuted.Should().Be( false );
		}

		[Fact]
		public void InterceptorCanPassActionThrough()
		{
			bool actionExecuted = false;

			IActiveAbility ability = CreateActiveAbility( phase: default, ( h, c ) => actionExecuted = true );
			IActionInterceptor interceptor = CreateInterceptor( ( a, c ) => a.AsOne() );

			Hero hero = new Hero( team: default, initialHp: default, ability, interceptor );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			actionExecuted.Should().Be( true );
		}

		[Fact]
		public void InterceptorCanReplaceAction()
		{
			bool originalActionExecuted = false;
			bool replacementActionExecuted = false;

			IActiveAbility ability = CreateActiveAbility( phase: default, ( h, c ) => originalActionExecuted = true );
			IActionInterceptor interceptor = CreateInterceptor( ( a, _ ) => CreateAction( c => replacementActionExecuted = true ).AsOne() );

			Hero hero = new Hero( team: default, initialHp: default, ability, interceptor );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			originalActionExecuted.Should().Be( false );
			replacementActionExecuted.Should().Be( true );
		}

		[Fact]
		public void InterceptorCanAddMultipleActions()
		{
			const int additionalActionsToCreate = 5;

			bool originalActionExecuted = false;
			int additionalActionsExecuted = 0;

			IActiveAbility ability = CreateActiveAbility( phase: default, ( h, c ) => originalActionExecuted = true );
			IActionInterceptor interceptor = CreateInterceptor
			(
				( a, _ )
				=> Enumerable
				.Repeat( CreateAction( c => additionalActionsExecuted++ ), additionalActionsToCreate )
				.Concat( a.AsArray() )
				.AsMany()
			);

			Hero hero = new Hero( team: default, initialHp: default, ability, interceptor );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService(), new RandomService( new Random() ) );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			originalActionExecuted.Should().Be( true );
			additionalActionsExecuted.Should().Be( additionalActionsToCreate );
		}
	}
}
