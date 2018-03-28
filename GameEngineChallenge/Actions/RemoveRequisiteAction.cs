namespace GameEngineChallenge.Actions
{
	public class RemoveRequisiteAction : IAction
	{
		public RemoveRequisiteAction( Hero hero, IRequisite requisite )
		{
			_hero = hero;
			_requisite = requisite;
		}

		private readonly Hero _hero;
		private readonly IRequisite _requisite;

		public void Execute( GameContext context ) => _hero.Requisites.Remove( _requisite );
	}
}
