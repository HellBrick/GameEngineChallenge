using Utils.SpaceTime;

namespace GameEngineChallenge.Actions
{
	public class MoveAction : IAction
	{
		public MoveAction( Hero hero, Vector moveVector )
		{
			Hero = hero;
			MoveVector = moveVector;
		}

		public Hero Hero { get; }
		public Vector MoveVector { get; }

		public void Execute( GameContext context ) => context.SpaceService.MoveHero( Hero, MoveVector );
	}
}
