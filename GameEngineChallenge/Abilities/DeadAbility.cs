using Utils;

namespace GameEngineChallenge.Abilities
{
	public class DeadAbility : IActionInterceptor
	{
		public static DeadAbility Instance { get; } = new DeadAbility();

		public RequisiteId Id => new RequisiteId( nameof( DeadAbility ) );
		public OneOrMany<IAction> Intercept( IAction action, GameContext context ) => OneOrMany<IAction>.Empty;
	}
}
