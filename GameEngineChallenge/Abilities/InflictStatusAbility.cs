using System;
using System.Collections.Generic;
using GameEngineChallenge.Actions;
using Utils;

namespace GameEngineChallenge.Abilities
{
	public class InflictStatusAbility : IActionInterceptor
	{
		public InflictStatusAbility( IRequisite status, TimeSpan duration )
		{
			_status = status;
			_duration = duration;
			Id = new RequisiteId( "Inflict" + status.Id );
		}

		private readonly IRequisite _status;
		private readonly TimeSpan _duration;
		public RequisiteId Id { get; }

		public IEnumerable<IAction> Intercept( IAction action, GameContext context )
		{
			return
				action is DamageAction damageAction && !damageAction.Target.Requisites.Contains( _status )
				? AppendStatusAndRemover()
				: action.AsArray();

			IAction[] AppendStatusAndRemover()
				=> new IAction[]
				{
					action,
					new AddRequisiteAction( damageAction.Target, _status ),
					new AddRequisiteAction( damageAction.Target, new RemoveRequisiteAbility( _status, CommonTickPhases.PassiveAbilities ).Schedule( _duration ) )
				};
		}
	}
}
