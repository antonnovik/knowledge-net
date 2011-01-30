using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Knowledge.Prospector.Common
{
	[Serializable]
	public class Set<T> : Dictionary<T, bool>, ISet<T> where T : class
	{

		public Set()
			: base()
		{ }

		public Set(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{ }


		#region ISet Members

		public IEnumerable<T> GetItems()
		{
			return Keys;
		}

		public bool Add(T item)
		{
			if (item == null) return false;
			if (OnAdd != null)
				if (!OnAdd(item))
					return false;
			//	item.GenerateId();

			if (this.ContainsKey(item))
			{
				this[item] = true;
				return false;
			}
			else
			{
				this[item] = true;
				return true;
			}
		}

		protected void FastAdd(T item)
		{
			this[item] = true;
		}

		public void Add(T[] items)
		{
			if (items != null)
				foreach (T item in items)
				{
					if (OnAdd != null)
						if (!OnAdd(item))
							continue;
					//	item.GenerateId();
					Add(item);
				}
		}

		public void Add<M>(ISet<M> set) where M : class, T
		{
			if (set != null)
				foreach (T item in set.GetItems())
				{
					if (OnAdd != null)
						if (!OnAdd(item))
							continue;
					Add(item);
				}
		}

		public void Add(List<T> items)
		{
			this.Add(items.ToArray());
		}

		public event SetEvent<T> OnAdd;

		public bool Delete(T item)
		{
			if (item == null) return false;
			if (OnDelete != null)
				if (!OnDelete(item))
					return false;
			return base.Remove(item);
		}

		public event SetEvent<T> OnDelete;

		#endregion

		public ISet<M> ConvertAll<M>(Converter<T, M> converter) where M : class
		{
			Set<M> result = new Set<M>();
			foreach (T item in GetItems())
				result.FastAdd(converter(item));

			return result;
		}
	}
}
