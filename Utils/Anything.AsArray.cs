namespace Utils
{
	public static partial class CollectionExtensions
	{
		public static T[] AsArray<T>( this T item ) => new T[] { item };
	}
}
