using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameEngineChallenge.Services;
using Utils;
using Utils.SpaceTime;

namespace GameEngineChallenge
{
	public static class Simulation
	{
		public static (Hero Hero, Position Position)[] GenerateStartingHeroes
		(
			Random random,
			double startingAreaSize,
			int teams,
			int heroesPerTeam,
			Func<TeamId, Hero>[] heroFactoryPool
		)
		{
			return
				Enumerable
				.Range( 0, teams )
				.Select( teamIndex => new TeamId( teamIndex ) )
				.SelectMany( team => Enumerable.Repeat( team, heroesPerTeam ) )
				.Select( team => (GenerateHero( team ), SelectPosition()) )
				.ToArray();

			Hero GenerateHero( TeamId teamId ) => heroFactoryPool[ random.Next( heroFactoryPool.Length ) ]( teamId );

			Position SelectPosition() => new Position( random.NextDouble() * startingAreaSize, random.NextDouble() * startingAreaSize );
		}

		public static GameContext Run( Random random, Func<GameContext, bool> gameOverCondition, params (Hero Hero, Position Position)[] startingHeroes )
		{
			HeroService heroService = new HeroService( startingHeroes.Select( h => h.Hero ).ToArray() );
			InputService inputService = new InputService();
			TimeService timeService = new TimeService();

			SpaceService spaceService = new SpaceService();
			foreach ( (Hero hero, Position position) in startingHeroes )
				spaceService.SetHeroPosition( hero, position );

			GameContext context = new GameContext( heroService, inputService, timeService, spaceService, new RandomService( random ) );

			TickExecutor tickExecutor = new TickExecutor();
			Stopwatch clock = Stopwatch.StartNew();

			while ( !gameOverCondition( context ) )
			{
				timeService.TimeElapsedSinceLastTick = clock.Elapsed;
				clock.Restart();

				SetInputs();
				tickExecutor.ExecuteTick( context );
			}

			return context;

			void SetInputs()
			{
				heroService.Heroes.ForEach( hero => inputService.SetDirection( hero, SelectDirection( hero ) ) );

				Vector SelectDirection( Hero hero )
				{
					Position heroPosition = spaceService.GetHeroPosition( hero );
					Position destination = GetDestination();
					return destination - heroPosition;

					Position GetDestination()
					{
						(Hero closestOpponent, Position opponentPosition, _)
							= EnumerateOpponentPlacements()
							.OrderBy( o => o.Distance )
							.FirstOrDefault();

						Position zeroPoint = new Position( 0.0, 0.0 );

						return
							closestOpponent == null
							? zeroPoint
							: MiddleBetween( opponentPosition, zeroPoint );

						IEnumerable<(Hero Hero, Position Position, Distance Distance)> EnumerateOpponentPlacements()
							=>
							from opponent in heroService.Heroes
							where opponent.Team != hero.Team
							let position = spaceService.GetHeroPosition( opponent )
							let distance = Distance.Between( heroPosition, position )
							select (opponent, position, distance);

						Position MiddleBetween( Position position1, Position position2 ) => position1 + ( position2 - position1 ) * 0.5;
					}
				}
			}
		}
	}
}
