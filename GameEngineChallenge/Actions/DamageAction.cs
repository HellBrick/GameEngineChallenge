namespace GameEngineChallenge.Actions
{
	public class DamageAction : IAction
	{
		public DamageAction( Hero target, HitPoints damage )
		{
			Target = target;
			Damage = damage;
		}

		public Hero Target { get; }
		public HitPoints Damage { get; }

		public void Execute( GameContext context )
		{
			Target.HP = new HitPoints( GetNewHpValue() );

			uint GetNewHpValue() => Target.HP < Damage ? 0 : Target.HP.Value - Damage.Value;
		}
	}
}
