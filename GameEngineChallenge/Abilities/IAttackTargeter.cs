using System.Collections.Generic;

namespace GameEngineChallenge.Abilities
{
	public interface IAttackTargeter
	{
		IEnumerable<Hero> EnumerateTargets( Hero attacker, GameContext context );
	}
}
