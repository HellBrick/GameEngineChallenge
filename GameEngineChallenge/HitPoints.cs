using System;
using System.Collections.Generic;

namespace GameEngineChallenge
{
	public readonly struct HitPoints : IEquatable<HitPoints>, IComparable<HitPoints>
	{
		public static HitPoints Zero { get; } = new HitPoints( 0 );

		public HitPoints( uint value ) => Value = value;

		public uint Value { get; }

		public override string ToString() => $"{Value} HP";

		public override int GetHashCode() => EqualityComparer<uint>.Default.GetHashCode( Value );
		public bool Equals( HitPoints other ) => EqualityComparer<uint>.Default.Equals( Value, other.Value );
		public override bool Equals( object obj ) => obj is GameEngineChallenge.HitPoints other && Equals( other );

		public static bool operator ==( HitPoints x, HitPoints y ) => x.Equals( y );
		public static bool operator !=( HitPoints x, HitPoints y ) => !x.Equals( y );

		public int CompareTo( HitPoints other ) => Value.CompareTo( other.Value );

		public static bool operator <( HitPoints x, HitPoints y ) => x.Value < y.Value;
		public static bool operator >( HitPoints x, HitPoints y ) => x.Value > y.Value;
		public static bool operator <=( HitPoints x, HitPoints y ) => x.Value <= y.Value;
		public static bool operator >=( HitPoints x, HitPoints y ) => x.Value >= y.Value;
	}
}
