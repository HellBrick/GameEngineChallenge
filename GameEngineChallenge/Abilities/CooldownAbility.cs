using System;
using System.Collections.Generic;

namespace GameEngineChallenge.Abilities
{
	public static partial class ActiveAbilityExtensions
	{
		public static CooldownAbility WithCooldown( this IActiveAbility activeAbility, TimeSpan cooldown )
			=> new CooldownAbility( activeAbility, cooldown );
	}

	public class CooldownAbility : IActiveAbility, ITimer
	{
		public CooldownAbility( IActiveAbility innerAbility, TimeSpan cooldown )
		{
			InnerAbility = innerAbility;
			_cooldown = cooldown;

			TimeLeft = TimeSpan.Zero;
		}

		private readonly TimeSpan _cooldown;

		public IActiveAbility InnerAbility { get; }
		public TimeSpan TimeLeft { get; set; }

		public RequisiteId Id => InnerAbility.Id;
		public TickPhase Phase => InnerAbility.Phase;

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
		{
			if ( TimeLeft > TimeSpan.Zero )
				return Array.Empty<IAction>();

			IReadOnlyCollection<IAction> actions = InnerAbility.GetActions( abilityOwner, context );
			if ( actions.Count > 0 )
				TimeLeft = _cooldown;

			return actions;
		}
	}
}
