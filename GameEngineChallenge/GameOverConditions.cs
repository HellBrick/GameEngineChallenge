using System.Linq;
using GameEngineChallenge.Abilities;

namespace GameEngineChallenge
{
	public static class GameOverConditions
	{
		public static bool OneTeamLeftAlive( GameContext context )
			=> context.HeroService.Heroes
			.Where( h => !h.Requisites.Contains( DeadAbility.Instance ) )
			.Select( h => h.Team )
			.Distinct()
			.Count() <= 1;
	}
}
