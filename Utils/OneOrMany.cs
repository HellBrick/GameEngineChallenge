using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
	public static class OneOrMany
	{
		public static OneOrMany<T> AsOne<T>( this T item ) where T : class => new OneOrMany<T>( item );
		public static OneOrMany<T> AsMany<T>( this IEnumerable<T> items ) where T : class => new OneOrMany<T>( items );
	}

	/// <summary>
	/// A collection type that optimizes away unnecessary allocations for the cases when the collection consists of 0 or 1 items.
	/// Can't store nulls.
	/// </summary>
	public readonly struct OneOrMany<T> : IEquatable<OneOrMany<T>>
		where T : class
	{
		public static OneOrMany<T> Empty => default;

		public OneOrMany( T item )
			=> _storage = item ?? throw new ArgumentNullException( _argumentNullMessage, nameof( item ) );

		public OneOrMany( IEnumerable<T> items )
			=> _storage = items ?? throw new ArgumentNullException( _argumentNullMessage, nameof( items ) );

		private static readonly string _argumentNullMessage = $"Can't store null, use {nameof( OneOrMany )}.{nameof( Empty )} instead.";
		private readonly object _storage;

		public OneOrMany<TOut> Select<TOut>( Func<T, TOut> selector )
			where TOut : class
			=> _storage is T one ? new OneOrMany<TOut>( selector( one ) )
			: _storage is IEnumerable<T> many ? new OneOrMany<TOut>( many.Select( selector ) )
			: OneOrMany<TOut>.Empty;

		public OneOrMany<TOut> SelectMany<TOut>( Func<T, OneOrMany<TOut>> selector )
			where TOut : class
		{
			return
				_storage is T one ? selector( one )
				: _storage is IEnumerable<T> many ? new OneOrMany<TOut>( EnumerateManyToMany() )
				: OneOrMany<TOut>.Empty;

			IEnumerable<TOut> EnumerateManyToMany()
			{
				foreach ( T item in many )
				{
					OneOrMany<TOut> outItems = selector( item );

					if ( outItems._storage is TOut oneOutItem )
					{
						yield return oneOutItem;
					}
					else if ( outItems._storage is IEnumerable<TOut> manyOutItems )
					{
						foreach ( TOut outItem in manyOutItems )
							yield return outItem;
					}
				}
			}
		}

		public IEnumerable<T> ToEnumerable()
			=> _storage is T one ? one.AsArray()
			: _storage is IEnumerable<T> many ? many
			: Enumerable.Empty<T>();

		public override int GetHashCode() => EqualityComparer<object>.Default.GetHashCode( _storage );
		public bool Equals( OneOrMany<T> other ) => EqualityComparer<object>.Default.Equals( _storage, other._storage );
		public override bool Equals( object obj ) => obj is Utils.OneOrMany<T> other && Equals( other );

		public static bool operator ==( OneOrMany<T> x, OneOrMany<T> y ) => x.Equals( y );
		public static bool operator !=( OneOrMany<T> x, OneOrMany<T> y ) => !x.Equals( y );
	}
}
