using System.Collections.Generic;
using GameEngineChallenge.Actions;
using Utils;

namespace GameEngineChallenge.Abilities
{
	public class RemoveRequisiteAbility : IActiveAbility
	{
		public RemoveRequisiteAbility( IRequisite requisiteToRemove, TickPhase phase )
		{
			_requisiteToRemove = requisiteToRemove;
			Phase = phase;
			Id = new RequisiteId( "Remove" + requisiteToRemove.Id );
		}

		private readonly IRequisite _requisiteToRemove;
		public TickPhase Phase { get; }
		public RequisiteId Id { get; }

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
			=> new IAction[]
			{
				new RemoveRequisiteAction( abilityOwner, _requisiteToRemove ),
				new RemoveRequisiteAction( abilityOwner, this )
			};
	}
}
