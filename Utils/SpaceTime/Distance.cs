using System;
using System.Collections.Generic;

namespace Utils.SpaceTime
{
	public readonly struct Distance : IEquatable<Distance>, IComparable<Distance>
	{
		public Distance( double meters ) => Meters = meters;

		public double Meters { get; }

		public static Distance Between( Position position1, Position position2 )
			=> new Distance
			(
				Math.Sqrt
				(
					Math.Pow( position1.X - position2.X, 2 ) + Math.Pow( position1.Y - position2.Y, 2 )
				)
			);

		public override string ToString() => $"{Meters} m";

		public override int GetHashCode() => EqualityComparer<double>.Default.GetHashCode( Meters );
		public bool Equals( Distance other ) => Meters == other.Meters;
		public override bool Equals( object obj ) => obj is Distance other && Equals( other );

		public static bool operator ==( Distance x, Distance y ) => x.Equals( y );
		public static bool operator !=( Distance x, Distance y ) => !x.Equals( y );

		public int CompareTo( Distance other ) => Meters.CompareTo( other.Meters );
		public static bool operator <( Distance x, Distance y ) => x.CompareTo( y ) < 0;
		public static bool operator >( Distance x, Distance y ) => x.CompareTo( y ) > 0;
		public static bool operator <=( Distance x, Distance y ) => x.CompareTo( y ) <= 0;
		public static bool operator >=( Distance x, Distance y ) => x.CompareTo( y ) >= 0;
	}
}
