using System.Collections.Generic;

namespace GameEngineChallenge.Abilities
{
	public interface ITargeter
	{
		IEnumerable<Hero> EnumerateTargets( Hero actor, GameContext context );
	}
}
