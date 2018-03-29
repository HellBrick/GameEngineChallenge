using Utils;

namespace GameEngineChallenge
{
	public interface IActionInterceptor : IRequisite
	{
		OneOrMany<IAction> Intercept( IAction action, GameContext context );
	}
}
