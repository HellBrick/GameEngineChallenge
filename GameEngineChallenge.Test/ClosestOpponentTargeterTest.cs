using System.Linq;
using FluentAssertions;
using GameEngineChallenge.Abilities.AttackTargeters;
using GameEngineChallenge.Services;
using Utils.SpaceTime;
using Xunit;

namespace GameEngineChallenge.Test
{
	public class ClosestOpponentTargeterTest
	{
		private static readonly HitPoints _initialHp = new HitPoints( 50 );

		[Fact]
		public void CanNotTargetFarAwayOpponent()
		{
			ClosestOpponentTargeter targeter = new ClosestOpponentTargeter( new Distance( 1.0 ) );

			Hero attacker = new Hero( new TeamId( 0 ), _initialHp );
			Hero opponent = new Hero( new TeamId( 1 ), _initialHp );

			HeroService heroService = new HeroService( new[] { attacker, opponent } );
			SpaceService spaceService = new SpaceService();

			spaceService.SetHeroPosition( attacker, new Position( 0.0, 0.0 ) );
			spaceService.SetHeroPosition( opponent, new Position( 2.0, 0.0 ) );

			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), spaceService );
			Hero[] targets = targeter.EnumerateTargets( attacker, context ).ToArray();

			targets.Should().BeEmpty();
		}

		[Fact]
		public void CanNotTargetTeammate()
		{
			ClosestOpponentTargeter targeter = new ClosestOpponentTargeter( new Distance( 1.0 ) );

			Hero attacker = new Hero( new TeamId( 0 ), _initialHp );
			Hero teammate = new Hero( new TeamId( 0 ), _initialHp );

			HeroService heroService = new HeroService( new[] { attacker, teammate } );
			SpaceService spaceService = new SpaceService();

			spaceService.SetHeroPosition( attacker, new Position( 0.0, 0.0 ) );
			spaceService.SetHeroPosition( teammate, new Position( 0.5, 0.0 ) );

			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), spaceService );
			Hero[] targets = targeter.EnumerateTargets( attacker, context ).ToArray();

			targets.Should().BeEmpty();
		}

		[Fact]
		public void CanNotTargetOpponentWithNoHpLeft()
		{
			ClosestOpponentTargeter targeter = new ClosestOpponentTargeter( new Distance( 1.0 ) );

			Hero attacker = new Hero( new TeamId( 0 ), _initialHp );
			Hero deadOpponent = new Hero( new TeamId( 1 ), new HitPoints( 0 ) );

			HeroService heroService = new HeroService( new[] { attacker, deadOpponent } );
			SpaceService spaceService = new SpaceService();

			spaceService.SetHeroPosition( attacker, new Position( 0.0, 0.0 ) );
			spaceService.SetHeroPosition( deadOpponent, new Position( 0.0, 0.5 ) );

			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), spaceService );
			Hero[] targets = targeter.EnumerateTargets( attacker, context ).ToArray();

			targets.Should().BeEmpty();
		}

		[Fact]
		public void ChoosesCorrectTarget()
		{
			ClosestOpponentTargeter targeter = new ClosestOpponentTargeter( new Distance( 1.0 ) );

			Hero attacker = new Hero( new TeamId( 0 ), _initialHp );
			Hero teammate = new Hero( new TeamId( 0 ), _initialHp );
			Hero deadOpponent = new Hero( new TeamId( 1 ), new HitPoints( 0 ) );
			Hero farOpponent = new Hero( new TeamId( 2 ), _initialHp );
			Hero closeOpponent = new Hero( new TeamId( 3 ), _initialHp );

			HeroService heroService = new HeroService( new[] { attacker, teammate, deadOpponent, farOpponent, closeOpponent } );
			SpaceService spaceService = new SpaceService();

			spaceService.SetHeroPosition( attacker, new Position( 0.0, 0.0 ) );
			spaceService.SetHeroPosition( teammate, new Position( 0.1, 0.0 ) );
			spaceService.SetHeroPosition( deadOpponent, new Position( 0.0, 0.2 ) );
			spaceService.SetHeroPosition( farOpponent, new Position( 0.0, 0.9 ) );
			spaceService.SetHeroPosition( closeOpponent, new Position( 0.3, 0.4 ) );

			GameContext context = new GameContext( heroService, new InputService(), new TimeService(), spaceService );
			Hero[] targets = targeter.EnumerateTargets( attacker, context ).ToArray();

			targets.Should().HaveCount( 1 );
			targets.Should().HaveElementAt( 0, closeOpponent );
		}
	}
}
