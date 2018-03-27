using FluentAssertions;
using GameEngineChallenge.Actions;
using GameEngineChallenge.Services;
using Utils;
using Xunit;

namespace GameEngineChallenge.Test
{
	public class AddRequisiteActionTest
	{
		[Fact]
		public void RequisitesWithTheSameIdDontStack()
		{
			BoringRequisite requisite = new BoringRequisite();
			Hero hero = new Hero( team: default, initialHp: default, requisite );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService() );

			new AddRequisiteAction( hero, requisite ).Execute( context );

			hero.Requisites.Count.Should().Be( 1 );
			hero.Requisites.Should().HaveElementAt( 0, requisite );
		}

		private class BoringRequisite : IRequisite
		{
			public RequisiteId Id => new RequisiteId( nameof( BoringRequisite ) );
		}
	}
}
