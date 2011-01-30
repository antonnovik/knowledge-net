using System;
using System.Collections.Generic;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Collections
{
	[Serializable]
	public class EntityList<T> : List<T>, IEntityList<T> where T : class, IEntity
	{
		public EntityList(List<T> list)
			: base(list)
		{ }

		public EntityList() { }

		public EntityList(int capacity)
			: base(capacity)
		{
			for (int i = 0; i < capacity; i++)
				base.Add(null);
		}


		#region IEntityList Members

		public IEntityList<T>[] Split(T[] separators)
		{
			IList<T> listOfSeparators = new List<T>(separators);
			List<IEntityList<T>> result = new List<IEntityList<T>>();
			IEntityList<T> entityList = new EntityList<T>();

			foreach (T entity in this)
			{
				entityList.Add(entity);

				if (listOfSeparators.Contains(entity))
				{
					result.Add(entityList);
					entityList = new EntityList<T>();
				}
			}
			result.Add(entityList);
			return result.ToArray();
		}

		public T[] FindAllByType(Type type)
		{
			List<T> result = new List<T>();
			foreach (T entity in this)
			{
				if (type.IsAssignableFrom(entity.GetType()))
					result.Add(entity);
			}
			return result.ToArray();
		}

		public new IEntityList<T> GetRange(int index, int count)
		{
			return new EntityList<T>(((List<T>)this).GetRange(index, count));
		}

		public new int IndexOf(T entity)
		{
			return ((List<T>)this).IndexOf(entity);
		}

		public IEntityList<T> GetLeftPartFrom(T entity)
		{
			try
			{
				return this.GetRange(0, IndexOf(entity));
			}
			catch
			{
				return new EntityList<T>();
			}
		}

		public IEntityList<T> GetRightPartFrom(T entity)
		{
			try
			{
				return this.GetRange(IndexOf(entity), Count - IndexOf(entity));
			}
			catch
			{
				return new EntityList<T>();
			}
		}

		public IEntityList<ITrueEntity> ConvertToTrueEntityList()
		{
			EntityList<ITrueEntity> result = new EntityList<ITrueEntity>();
			foreach (T entity in this)
			{
				ITrueEntity trueEntity;
				if(TrueEntity.TryToTrueEntity(entity, out trueEntity))
					result.Add(trueEntity);
			}
			return result;			
		}

		#endregion
	}
}
