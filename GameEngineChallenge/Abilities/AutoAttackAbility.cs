using System.Collections.Generic;
using System.Linq;
using GameEngineChallenge.Actions;

namespace GameEngineChallenge.Abilities
{
	public class AutoAttackAbility : IActiveAbility
	{
		public AutoAttackAbility( HitPoints damage, ITargeter targeter )
		{
			_damage = damage;
			_targeter = targeter;
		}

		private readonly HitPoints _damage;
		private readonly ITargeter _targeter;

		public TickPhase Phase => CommonTickPhases.OffensiveAbilities;
		public RequisiteId Id => new RequisiteId( nameof( AutoAttackAbility ) );

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
			=> _targeter.EnumerateTargets( abilityOwner, context )
			.Select( target => new DamageAction( target, _damage ) as IAction )
			.ToArray();
	}
}
