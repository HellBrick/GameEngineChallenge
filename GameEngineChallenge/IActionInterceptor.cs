using System.Collections.Generic;

namespace GameEngineChallenge
{
	public interface IActionInterceptor : IRequisite
	{
		IEnumerable<IAction> Intercept( IAction action, GameContext context );
	}
}
