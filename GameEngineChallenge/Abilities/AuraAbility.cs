using System.Collections.Generic;
using System.Linq;
using GameEngineChallenge.Actions;

namespace GameEngineChallenge.Abilities
{
	public class AuraAbility : IActiveAbility
	{
		public AuraAbility( IRequisite inflictedRequisite, ITargeter targeter )
		{
			_inflictedRequisite = inflictedRequisite;
			_targeter = targeter;
			Id = new RequisiteId( inflictedRequisite.Id + "Aura" );
		}

		private readonly IRequisite _inflictedRequisite;
		private readonly ITargeter _targeter;

		public TickPhase Phase => CommonTickPhases.PassiveAbilities;
		public RequisiteId Id { get; }

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
			=> _targeter.EnumerateTargets( abilityOwner, context )
			.Where( target => !target.Requisites.Contains( _inflictedRequisite ) )
			.SelectMany
			(
				target => new IAction[]
				{
					new AddRequisiteAction( target, _inflictedRequisite ),
					new AddRequisiteAction( target, new RemoveRequisiteAbility( _inflictedRequisite, CommonTickPhases.CleanUp ) )
				}
			)
			.ToArray();
	}
}
