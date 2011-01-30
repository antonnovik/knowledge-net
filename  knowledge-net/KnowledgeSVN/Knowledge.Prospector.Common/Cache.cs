using System;
using System.Collections.Generic;

namespace Knowledge.Prospector.Common
{
	/// <summary>
	/// Simple cache.
	/// </summary>
	/// <typeparam name="TKey">Type of keys.</typeparam>
	/// <typeparam name="TValue">Type of values.</typeparam>
	public class Cache<TKey, TValue>
	{
		/// <summary>
		/// Store for values.
		/// </summary>
		private Dictionary<TKey, TValue> Store;

		/// <summary>
		/// Delegate for obtaining items not cantained in cache.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public delegate TValue ObtainItemDelegate(TKey key);

		private ObtainItemDelegate ObtainItem;

		/// <summary>
		/// Initialize a new instance of Cache.
		/// </summary>
		/// <param name="obtainItem">Used for obtaining new items. Cannot be null.</param>
		/// <exception cref="System.NullReferenceException">Thrown when obtainItem is null.</exception>
		public Cache(ObtainItemDelegate obtainItem)
		{
			if(obtainItem == null)
				throw new NullReferenceException("obtainItem cannot be null");

			ObtainItem = obtainItem;
			Store = new Dictionary<TKey, TValue>();
		}

		/// <summary>
		/// Gets item from cache.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <remarks>
		/// If there is no such item in Store, ObtainItem will be used for retrieving item.
		/// New item will be added to Store.
		/// </remarks>
		public TValue GetItem(TKey key)
		{
			if (!Store.ContainsKey(key))
				Store[key] = ObtainItem(key);
			return Store[key];
		}

		/// <summary>
		/// Removes all items from cache.
		/// </summary>
		public void Clear()
		{
			Store.Clear();
		}
	}
}
