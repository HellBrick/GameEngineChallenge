using System;
using System.Collections.Generic;

namespace Utils.SpaceTime
{
	public readonly struct Speed : IEquatable<Speed>
	{
		public Speed( double metersPerSecond ) => MetersPerSecond = metersPerSecond;

		public double MetersPerSecond { get; }

		public static Distance operator *( Speed speed, TimeSpan time ) => new Distance( speed.MetersPerSecond * time.TotalSeconds );
		public static Distance operator *( TimeSpan time, Speed speed ) => speed * time;

		public override string ToString() => $"{MetersPerSecond} m/s";

		public override int GetHashCode() => EqualityComparer<double>.Default.GetHashCode( MetersPerSecond );
		public bool Equals( Speed other ) => MetersPerSecond == other.MetersPerSecond;
		public override bool Equals( object obj ) => obj is Speed other && Equals( other );

		public static bool operator ==( Speed x, Speed y ) => x.Equals( y );
		public static bool operator !=( Speed x, Speed y ) => !x.Equals( y );
	}
}
