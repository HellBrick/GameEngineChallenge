using System;
using System.Collections.Generic;

namespace GameEngineChallenge
{
	public readonly struct RequisiteId : IEquatable<RequisiteId>
	{
		public RequisiteId( string id ) => Id = id;

		public string Id { get; }

		public override string ToString() => Id;

		public override int GetHashCode() => EqualityComparer<string>.Default.GetHashCode( Id );
		public bool Equals( RequisiteId other ) => Id == other.Id;
		public override bool Equals( object obj ) => obj is GameEngineChallenge.RequisiteId other && Equals( other );

		public static bool operator ==( RequisiteId x, RequisiteId y ) => x.Equals( y );
		public static bool operator !=( RequisiteId x, RequisiteId y ) => !x.Equals( y );
	}
}
