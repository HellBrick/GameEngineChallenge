using System;
using System.Collections.Generic;

namespace Utils
{
	public static partial class EnumerableExtensions
	{
		public static T? MinOrNull<T>( this IEnumerable<T> sequence )
			where T : struct, IComparable<T>
		{
			using ( IEnumerator<T> enumerator = sequence.GetEnumerator() )
			{
				if ( !enumerator.MoveNext() )
					return default;

				T minValue = enumerator.Current;

				while ( enumerator.MoveNext() )
				{
					T currentValue = enumerator.Current;
					if ( currentValue.CompareTo( minValue ) < 0 )
						minValue = currentValue;
				}

				return minValue;
			}
		}
	}
}
