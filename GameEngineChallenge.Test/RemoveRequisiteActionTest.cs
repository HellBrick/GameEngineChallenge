using FluentAssertions;
using GameEngineChallenge.Actions;
using GameEngineChallenge.Services;
using Utils;
using Xunit;

namespace GameEngineChallenge.Test
{
	public class RemoveRequisiteActionTest
	{
		[Fact]
		public void RemovingNonExistingRequisiteIsNoop()
		{
			ExistingRequisite existingRequisite = new ExistingRequisite();
			Hero hero = new Hero( team: default, existingRequisite );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService() );

			new RemoveRequisiteAction( hero, new NonExistingRequisite() ).Execute( context );

			hero.Requisites.Count.Should().Be( 1 );
			hero.Requisites.Should().HaveElementAt( 0, existingRequisite );
		}

		[Fact]
		public void RemovingExistingRequisiteWorks()
		{
			ExistingRequisite existingRequisite = new ExistingRequisite();
			Hero hero = new Hero( team: default, existingRequisite );
			HeroService heroService = new HeroService( hero.AsArray() );
			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), new SpaceService() );

			new RemoveRequisiteAction( hero, existingRequisite ).Execute( context );

			hero.Requisites.Count.Should().Be( 0 );
		}

		private class ExistingRequisite : IRequisite
		{
			public RequisiteId Id => new RequisiteId( nameof( ExistingRequisite ) );
		}

		private class NonExistingRequisite : IRequisite
		{
			public RequisiteId Id => new RequisiteId( nameof( NonExistingRequisite ) );
		}
	}
}
