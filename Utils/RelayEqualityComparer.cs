using System;
using System.Collections.Generic;

namespace Utils
{
	public static class RelayEqualityComparer
	{
		public static IEqualityComparer<TItem> Create<TItem, TKey>( Func<TItem, TKey> keySelector )
			=> Create( keySelector, EqualityComparer<TKey>.Default );

		public static IEqualityComparer<TItem> Create<TItem, TKey>( Func<TItem, TKey> keySelector, IEqualityComparer<TKey> keyComparer )
			=> new Implementation<TItem, TKey>( keySelector, keyComparer );

		private class Implementation<TItem, TKey> : IEqualityComparer<TItem>
		{
			private readonly Func<TItem, TKey> _keySelector;
			private readonly IEqualityComparer<TKey> _keyComparer;

			public Implementation( Func<TItem, TKey> keySelector, IEqualityComparer<TKey> keyComparer )
			{
				_keySelector = keySelector;
				_keyComparer = keyComparer;
			}

			public bool Equals( TItem x, TItem y ) => _keyComparer.Equals( _keySelector( x ), _keySelector( y ) );
			public int GetHashCode( TItem obj ) => _keyComparer.GetHashCode( _keySelector( obj ) );
		}
	}
}
