using System;
using System.Collections.Generic;

namespace Utils
{
	public static partial class EnumerableExtensions
	{
		public static void ForEach<TItem>( this IEnumerable<TItem> sequence, Action<TItem> action )
			=> sequence.ForEach( action, ( capturedAction, item ) => capturedAction( item ) );

		public static void ForEach<TItem, TCapture>( this IEnumerable<TItem> sequence, TCapture capture, Action<TCapture, TItem> action )
		{
			foreach ( TItem item in sequence )
				action( capture, item );
		}
	}
}
