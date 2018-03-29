using System;
using System.Collections.Generic;
using System.Linq;
using GameEngineChallenge.Actions;
using Utils;

namespace GameEngineChallenge.Abilities
{
	public static partial class ActiveAbilityExtensions
	{
		public static ScheduledAbility Schedule( this IActiveAbility activeAbility, TimeSpan delay )
			=> new ScheduledAbility( activeAbility, delay );
	}

	public class ScheduledAbility : IActiveAbility, ITimer
	{
		public ScheduledAbility( IActiveAbility innerAbility, TimeSpan delay )
		{
			InnerAbility = innerAbility;
			TimeLeft = delay;
			Id = new RequisiteId( "Scheduled" + innerAbility.Id.Id );
		}

		public TickPhase Phase => InnerAbility.Phase;
		public RequisiteId Id { get; }

		public IActiveAbility InnerAbility { get; }
		public TimeSpan TimeLeft { get; set; }

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
		{
			return
				TimeLeft > TimeSpan.Zero
				? Array.Empty<IAction>()
				: GetInnerActionsAndSelfRemover();

			IReadOnlyCollection<IAction> GetInnerActionsAndSelfRemover()
				=> InnerAbility
				.GetActions( abilityOwner, context )
				.Concat( new RemoveRequisiteAction( abilityOwner, this ).AsArray() )
				.ToArray();
		}
	}
}
