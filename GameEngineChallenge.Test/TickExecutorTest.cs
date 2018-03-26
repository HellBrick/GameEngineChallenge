using System.Collections.Generic;
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

			Hero hero = new Hero( new IRequisite[] { CreatePhaseAbility( phase1 ), CreatePhaseAbility( phase4 ), CreatePhaseAbility( phase0 ) } );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService );

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

			Hero hero = new Hero( newAbilityInflicter );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService );

			TickExecutor executor = new TickExecutor();
			executor.ExecuteTick( context );

			newAbilityExecuted.Should().Be( shouldExecuteNewAbility );
		}
	}
}
