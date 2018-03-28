using System.Collections.Generic;
using System.Linq;

namespace GameEngineChallenge.Abilities
{
	public class DeadAbility : IActionInterceptor
	{
		public static DeadAbility Instance { get; } = new DeadAbility();

		public RequisiteId Id => new RequisiteId( nameof( DeadAbility ) );
		public IEnumerable<IAction> Intercept( IAction action ) => Enumerable.Empty<IAction>();
	}
}
