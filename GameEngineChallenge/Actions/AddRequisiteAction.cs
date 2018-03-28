namespace GameEngineChallenge.Actions
{
	public class AddRequisiteAction : IAction
	{
		public AddRequisiteAction( Hero hero, IRequisite requisite )
		{
			_hero = hero;
			_requisite = requisite;
		}

		private readonly Hero _hero;
		private readonly IRequisite _requisite;

		public void Execute( GameContext context ) => _hero.Requisites.Add( _requisite );
	}
}
