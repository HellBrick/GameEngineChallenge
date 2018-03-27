using System;
using System.Collections.Generic;

namespace GameEngineChallenge
{
	public readonly struct TickPhase : IEquatable<TickPhase>, IComparable<TickPhase>
	{
		public TickPhase( uint order ) => Order = order;

		public uint Order { get; }

		public override string ToString() => $"Phase #{Order}";

		public override int GetHashCode() => EqualityComparer<uint>.Default.GetHashCode( Order );
		public bool Equals( TickPhase other ) => EqualityComparer<uint>.Default.Equals( Order, other.Order );
		public override bool Equals( object obj ) => obj is GameEngineChallenge.TickPhase other && Equals( other );

		public static bool operator ==( TickPhase x, TickPhase y ) => x.Equals( y );
		public static bool operator !=( TickPhase x, TickPhase y ) => !x.Equals( y );

		public int CompareTo( TickPhase other ) => Order.CompareTo( other.Order );

		public static bool operator <( TickPhase x, TickPhase y ) => x.CompareTo( y ) < 0;
		public static bool operator >( TickPhase x, TickPhase y ) => x.CompareTo( y ) > 0;
		public static bool operator <=( TickPhase x, TickPhase y ) => x.CompareTo( y ) <= 0;
		public static bool operator >=( TickPhase x, TickPhase y ) => x.CompareTo( y ) >= 0;
	}
}
