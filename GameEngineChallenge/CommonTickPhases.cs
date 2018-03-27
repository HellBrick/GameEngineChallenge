namespace GameEngineChallenge
{
	public static class CommonTickPhases
	{
		public static TickPhase TimeAdjustments { get; } = new TickPhase( 0 );
		public static TickPhase OffensiveAbilities { get; } = new TickPhase( TimeAdjustments.Order + 1 );
	}
}
