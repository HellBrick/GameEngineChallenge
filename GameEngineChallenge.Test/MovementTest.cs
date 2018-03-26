using System;
using FluentAssertions;
using GameEngineChallenge.Abilities;
using GameEngineChallenge.Services;
using Utils;
using Utils.SpaceTime;
using Xunit;

namespace GameEngineChallenge.Test
{
	public class MovementTest
	{
		[Theory]
		[InlineData( 155.0, 128.0, 0.0, 0.0, 0.0, 0.0 )]
		[InlineData( 0.0, 42.0, 1.0, 0.0, 0.0, 0.0 )]
		[InlineData( 2.0, 2.5, 1.0, 0.0, 5.0, 0.0 )]
		[InlineData( 3.0, 0.5, 0.0, 1.0, 0.0, 1.5 )]
		[InlineData( 4.0, 1.0, -1.0, 0.0, -4.0, 0.0 )]
		[InlineData( 5.0, 0.2, 0.0, -1.0, 0.0, -1.0 )]
		[InlineData( 6.0, 1.0, 3.0, 4.0, 3.6, 4.8 )]
		public void MovementMathChecksOut( double speed, double time, double inputX, double inputY, double positionX, double positionY )
		{
			Hero hero = new Hero( new InputBasedMovementAbility( new Speed( speed ) ) );
			HeroService heroService = new HeroService( hero.AsArray() );
			SpaceService spaceService = new SpaceService();
			InputService inputService = new InputService();
			TimeService timeService = new TimeService();
			GameContext context = new GameContext( heroService, inputService, timeService, spaceService );

			spaceService.SetHeroPosition( hero, new Position( 0.0, 0.0 ) );
			inputService.SetDirection( hero, new Vector( inputX, inputY ) );
			timeService.TimeElapsedSinceLastTick = TimeSpan.FromSeconds( time );

			new TickExecutor().ExecuteTick( context );

			Position position = spaceService.GetHeroPosition( hero );
			const double assertPrecision = 0.0001;
			position.X.Should().BeApproximately( positionX, assertPrecision );
			position.Y.Should().BeApproximately( positionY, assertPrecision );
		}
	}
}
