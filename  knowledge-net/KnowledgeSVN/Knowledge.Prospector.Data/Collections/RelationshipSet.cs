using System;
using System.Runtime.Serialization;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Collections
{
	[Serializable]
	public class RelationshipSet<T> : Set<T>, IRelationshipSet<T> where T : class, IRelationship
	{
		public RelationshipSet()
			: base()
		{ }

		public RelationshipSet(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{ }


		public IRelationshipSet<T> GetBeginningWith(ITrueEntity begin)
		{
			IRelationshipSet<T> temp = new RelationshipSet<T>();
			foreach (T t in this.GetItems())
			{
				if (t.Entities != null && t.Entities.Count != 0 && t.Entities[0].Equals(begin))
					temp.Add(t);
			}
			return temp;
		}

		public IRelationshipSet<T> GetEndingWith(ITrueEntity end)
		{
			IRelationshipSet<T> temp = new RelationshipSet<T>();
			foreach (T t in this.GetItems())
			{
				if (t.Entities != null && t.Entities.Count != 0 && t.Entities[1].Equals(end))
					temp.Add(t);
			}
			return temp;
		}

		public ISet<ITrueEntity> GetBeginEntities()
		{
			ISet<ITrueEntity> temp = new Set<ITrueEntity>();
			foreach (T t in this.GetItems())
			{
				if (t.Entities != null && t.Entities.Count != 0)
					temp.Add(t.Entities[0]);
			}
			return temp;
		}

		public ISet<ITrueEntity> GetEndEntities()
		{
			ISet<ITrueEntity> temp = new Set<ITrueEntity>();
			foreach (T t in this.GetItems())
			{
				if (t.Entities != null && t.Entities.Count > 1)
					temp.Add(t.Entities[1]);
			}
			return temp;
		}

		public new IRelationshipSet<M> ConvertAll<M>(Converter<T, M> converter) where M : class, IRelationship
		{
			RelationshipSet<M> result = new RelationshipSet<M>();
			foreach (T item in GetItems())
				result.FastAdd(converter(item));

			return result;
		}
	}
}
