namespace GameEngineChallenge.Actions
{
	public class DamageAction : IAction
	{
		public DamageAction( Hero target, HitPoints damage )
		{
			_target = target;
			_damage = damage;
		}

		private readonly Hero _target;
		private readonly HitPoints _damage;

		public void Execute( GameContext context )
		{
			_target.HP = new HitPoints( GetNewHpValue() );

			uint GetNewHpValue() => _target.HP < _damage ? 0 : _target.HP.Value - _damage.Value;
		}
	}
}
