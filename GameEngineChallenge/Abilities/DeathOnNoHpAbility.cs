using System;
using System.Collections.Generic;
using GameEngineChallenge.Actions;
using Utils;

namespace GameEngineChallenge.Abilities
{
	public class DeathOnNoHpAbility : IActiveAbility
	{
		public static DeathOnNoHpAbility Instance { get; } = new DeathOnNoHpAbility();

		public TickPhase Phase => CommonTickPhases.DeathCheck;
		public RequisiteId Id => new RequisiteId( nameof( DeathOnNoHpAbility ) );

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
			=> abilityOwner.HP <= HitPoints.Zero
			? new AddRequisiteAction( abilityOwner, DeadAbility.Instance ).AsArray()
			: Array.Empty<IAction>();
	}
}
