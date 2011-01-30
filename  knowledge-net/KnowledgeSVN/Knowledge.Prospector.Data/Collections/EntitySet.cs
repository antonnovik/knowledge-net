using System;
using System.Runtime.Serialization;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Collections
{
	[Serializable]
	public class EntitySet<T> : Set<T>, IEntitySet<T> where T : class, IEntity
	{
		#region IEntitySet Members

		public EntitySet()
			: base()
		{ }

		public EntitySet(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{ }

		public void Add(IEntityList<T> entityList)
		{
			if (entityList != null)
				foreach (T entity in entityList)
				{
					Add(entity);
				}
		}

		#endregion
	}
}