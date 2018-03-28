using Utils.SpaceTime;

namespace GameEngineChallenge.Actions
{
	public class MoveAction : IAction
	{
		private readonly Hero _hero;
		private readonly Vector _moveVector;

		public MoveAction( Hero hero, Vector moveVector )
		{
			_hero = hero;
			_moveVector = moveVector;
		}

		public void Execute( GameContext context ) => context.SpaceService.MoveHero( _hero, _moveVector );
	}
}
