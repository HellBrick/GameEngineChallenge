using System.Collections.Generic;
using System.Linq;
using GameEngineChallenge.Actions;

namespace GameEngineChallenge.Abilities
{
	public class DecreaseAllTimersAbility : IActiveAbility
	{
		public static DecreaseAllTimersAbility Instance { get; } = new DecreaseAllTimersAbility();

		public TickPhase Phase => CommonTickPhases.TimeAdjustments;
		public RequisiteId Id => new RequisiteId( nameof( DecreaseAllTimersAbility ) );

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
			=> abilityOwner
			.Requisites
			.OfType<ITimer>()
			.Select( timer => new DecreaseTimerAction( timer, context.TimeService.TimeElapsedSinceLastTick ) as IAction )
			.ToArray();
	}
}
