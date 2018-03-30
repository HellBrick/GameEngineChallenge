using System;
using System.Collections.Generic;

namespace Utils.SpaceTime
{
	public readonly struct Position : IEquatable<Position>
	{
		public Position( double x, double y )
		{
			X = x;
			Y = y;
		}

		public double X { get; }
		public double Y { get; }

		public static Position operator +( Position position, Vector vector ) => new Position( position.X + vector.X, position.Y + vector.Y );
		public static Vector operator -( Position position1, Position position2 ) => new Vector( position1.X - position2.X, position1.Y - position2.Y );

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

		public bool Equals( Position other ) => X == other.X && Y == other.Y;
		public override bool Equals( object obj ) => obj is Position other && Equals( other );

		public static bool operator ==( Position x, Position y ) => x.Equals( y );
		public static bool operator !=( Position x, Position y ) => !x.Equals( y );
	}
}
