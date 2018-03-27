using System.Collections.Generic;

namespace GameEngineChallenge
{
	public interface IActiveAbility : IRequisite
	{
		TickPhase Phase { get; }
		IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context );
	}
}
