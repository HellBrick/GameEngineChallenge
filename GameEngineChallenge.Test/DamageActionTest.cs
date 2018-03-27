using FluentAssertions;
using GameEngineChallenge.Actions;
using GameEngineChallenge.Services;
using Utils;
using Xunit;

namespace GameEngineChallenge.Test
{
	public class DamageActionTest
	{
		[Theory]
		[InlineData( 0, 0, 0 )]
		[InlineData( 42, 0, 42 )]
		[InlineData( 15, 15, 0 )]
		[InlineData( 13, 12, 1 )]
		[InlineData( 50, 9999, 0 )]
		public void DamageMathChecksOut( uint initialHp, uint damage, uint expectedHp )
		{
			Hero hero = new Hero( team: default, new HitPoints( initialHp ) );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService() );

			new DamageAction( hero, new HitPoints( damage ) ).Execute( context );

			hero.HP.Should().Be( new HitPoints( expectedHp ) );
		}
	}
}
