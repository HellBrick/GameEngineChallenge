using Utils.SpaceTime;

namespace GameEngineChallenge.Actions
{
	public class MoveAction : IAction
	{
		public Hero Hero { get; }
		public Vector MoveVector { get; }

		public MoveAction( Hero hero, Vector moveVector )
		{
			Hero = hero;
			MoveVector = moveVector;
		}

		public void Execute( GameContext context ) => context.SpaceService.MoveHero( Hero, MoveVector );
	}
}
