using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Utils;

namespace GameEngineChallenge.Simulator
{
	internal class Program
	{
		private static void Main( string[] args )
		{
			int seed = new Random().Next();
			Console.WriteLine( $"Seed: {seed}" );
			Random random = new Random( seed );

			(Hero Hero, Utils.SpaceTime.Position Position)[] startingHeroes = GetStartingHeroes();
			GameContext finalContext = Simulation.Run( random, c => GameOverConditions.OneTeamLeftAlive( c ), startingHeroes );

			finalContext
				.HeroService.Heroes
				.Select( ( hero, index ) => (hero, index) )
				.ForEach( heroIndex => Console.WriteLine( $"Team {heroIndex.hero.Team} / hero {heroIndex.index} : {heroIndex.hero.HP} left" ) );

			(Hero Hero, Utils.SpaceTime.Position Position)[] GetStartingHeroes()
			{
				return Simulation.GenerateStartingHeroes( random, startingAreaSize: 10.0, teams: 2, heroesPerTeam: 2, GetAllHeroFactories() );

				Func<TeamId, Hero>[] GetAllHeroFactories()
				{
					return
						typeof( HeroFactory )
						.GetMethods()
						.Where( m => m.ReturnType == typeof( Hero ) )
						.Where( m => m.GetParameters() is ParameterInfo[] parameters && parameters.Length == 1 && parameters[ 0 ].ParameterType == typeof( TeamId ) )
						.Select( m => CompileFactoryMethod( m ) )
						.ToArray();

					Func<TeamId, Hero> CompileFactoryMethod( MethodInfo methodInfo )
					{
						ParameterExpression teamParameter = Expression.Parameter( typeof( TeamId ) );
						MethodCallExpression methodCall = Expression.Call( instance: null, methodInfo, teamParameter );
						Expression<Func<TeamId, Hero>> lambda = Expression.Lambda<Func<TeamId, Hero>>( methodCall, teamParameter );
						return lambda.Compile();
					}
				}
			}
		}
	}
}
