using System;
using FluentAssertions;
using GameEngineChallenge.Actions;
using GameEngineChallenge.Services;
using Utils;
using Xunit;

namespace GameEngineChallenge.Test
{
	public class DecreaseTimerActionTest
	{
		[Theory]
		[InlineData( 0, 0, 0 )]
		[InlineData( 0, 1, 0 )]
		[InlineData( 1, 5, 0 )]
		[InlineData( 3, 1, 2 )]
		public void TimeLeftIsUpdatedCorrectly( int initialTimeLeft, int decrement, int expectedtimeLeft )
		{
			TimerRequisite timer = new TimerRequisite() { TimeLeft = TimeSpan.FromSeconds( initialTimeLeft ) };
			Hero hero = new Hero( team: default, initialHp: default, timer );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService() );

			new DecreaseTimerAction( timer, TimeSpan.FromSeconds( decrement ) ).Execute( context );

			timer.TimeLeft.Should().Be( TimeSpan.FromSeconds( expectedtimeLeft ) );
		}

		private class TimerRequisite : ITimer, IRequisite
		{
			public RequisiteId Id => new RequisiteId( nameof( TimerRequisite ) );
			public TimeSpan TimeLeft { get; set; }
		}
	}
}
