using System;
using System.Collections.Generic;

namespace GameEngineChallenge
{
	public readonly struct TeamId : IEquatable<TeamId>
	{
		public TeamId( int id ) => Id = id;

		public int Id { get; }

		public override string ToString() => Id.ToString();

		public override int GetHashCode() => EqualityComparer<int>.Default.GetHashCode( Id );
		public bool Equals( TeamId other ) => EqualityComparer<int>.Default.Equals( Id, other.Id );
		public override bool Equals( object obj ) => obj is TeamId other && Equals( other );

		public static bool operator ==( TeamId x, TeamId y ) => x.Equals( y );
		public static bool operator !=( TeamId x, TeamId y ) => !x.Equals( y );
	}
}
