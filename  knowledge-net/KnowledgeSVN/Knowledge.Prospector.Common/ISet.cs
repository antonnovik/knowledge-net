using System;
using System.Collections.Generic;

namespace Knowledge.Prospector.Common
{
	public delegate bool SetEvent<T>(T item);
	/// <summary>
	/// Set of entities. Set stores only reference to entities.
	/// </summary>
	public interface ISet<T> : IDictionary<T, bool> where T : class
	{
		/// <summary>
		/// Returns all items contained in set.
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetItems();

		/// <summary>
		/// Adds item to set.
		/// Before adding execute GenerateId() for item.
		/// </summary>
		/// <param name="item">Added item.</param>
		/// <returns>True if added instance is new, otherwise false.</returns>
		bool Add(T item);

		/// <summary>
		/// Adds array of items to set.
		/// Before adding execute GenerateId() for each item.
		/// </summary>
		/// <param name="items">Added array of items.</param>
		void Add(T[] items);

		/// <summary>
		/// Adds set of items to set.
		/// </summary>
		/// <param name="set">Added set of items.</param>
		void Add<M>(ISet<M> set) where M : class, T;

		/// <summary>
		/// Adds List of items to set.
		/// Before adding execute GenerateId() for each item.
		/// </summary>
		/// <param name="items"></param>
		void Add(List<T> items);

		/// <summary>
		/// Occures before adding new item to set.
		/// </summary>
		event SetEvent<T> OnAdd;

		/// <summary>
		/// Deletes the item from the set.
		/// Event OnDelete occures before deleting.
		/// </summary>
		/// <param name="item"></param>
		bool Delete(T item);

		/// <summary>
		/// Occures before deleting item from set.
		/// </summary>
		event SetEvent<T> OnDelete;

		/// <summary>
		/// Converts all items from set to another using converter.
		/// </summary>
		/// <typeparam name="M">Type, converts to.</typeparam>
		/// <param name="converter"></param>
		/// <returns></returns>
		ISet<M> ConvertAll<M>(Converter<T, M> converter) where M : class;
	}
}
