using System;
using System.Collections.Generic;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Collections
{
	/// <summary>
	/// List of entities.
	/// </summary>
	public interface IEntityList<T> : IList<T>
	{
		/// <summary>
		/// Return IEntityList array of sublist delimited by array of IEntity.
		/// </summary>
		/// <param name="separators">Array of separators.</param>
		/// <returns>Sublists delimited by separators.</returns>
		IEntityList<T>[] Split(T[] separators);

		/// <summary>
		/// Find all IEntity with specified Type.
		/// </summary>
		/// <param name="type">Type of looked for IEntity.</param>
		/// <returns>Array of IEntity.</returns>
		T[] FindAllByType(Type type);

		IEntityList<T> GetLeftPartFrom(T entity);
		IEntityList<T> GetRightPartFrom(T entity);
		IEntityList<T> GetRange(int index, int count);

		new int IndexOf(T entity);

		IEntityList<ITrueEntity> ConvertToTrueEntityList();

		T[] ToArray();
	}
}
