using System;
using System.Collections.Generic;

namespace Utils.SpaceTime
{
	public readonly struct Vector : IEquatable<Vector>
	{
		public Vector( double x, double y )
		{
			X = x;
			Y = y;
		}

		public double X { get; }
		public double Y { get; }

		public Vector Normalize()
		{
			if ( this == default )
				return default;

			const double normalizedVectorLength = 1.0;
			double hypotenuse = Math.Sqrt( X * X + Y * Y );
			double scaleFactor = normalizedVectorLength / hypotenuse;
			return this * scaleFactor;
		}

		public static Vector operator *( Vector vector, double multiplier ) => new Vector( vector.X * multiplier, vector.Y * multiplier );

		public override string ToString() => $"[{X} : {Y}]";

		public override int GetHashCode()
		{
			unchecked
			{
				const int prime = -1521134295;
				int hash = 12345701;
				hash = hash * prime + EqualityComparer<double>.Default.GetHashCode( X );
				hash = hash * prime + EqualityComparer<double>.Default.GetHashCode( Y );
				return hash;
			}
		}

		public bool Equals( Vector other ) => X == other.X && Y == other.Y;
		public override bool Equals( object obj ) => obj is Vector other && Equals( other );

		public static bool operator ==( Vector x, Vector y ) => x.Equals( y );
		public static bool operator !=( Vector x, Vector y ) => !x.Equals( y );
	}
}
