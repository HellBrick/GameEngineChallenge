﻿using System;

namespace GameEngineChallenge
{
	public static class CommonTickPhases
	{
		public static TickPhase DeathCheck { get; } = new TickPhase( 0 );
		public static TickPhase TimeAdjustments { get; } = new TickPhase( DeathCheck.Order + 1 );
		public static TickPhase PassiveAbilities { get; } = new TickPhase( TimeAdjustments.Order + 1 );
		public static TickPhase OffensiveAbilities { get; } = new TickPhase( PassiveAbilities.Order + 1 );
		public static TickPhase CleanUp { get; } = new TickPhase( Int32.MaxValue );
	}
}
