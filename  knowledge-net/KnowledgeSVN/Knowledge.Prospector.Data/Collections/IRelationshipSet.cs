using System;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Collections
{
	public interface IRelationshipSet<T> : ISet<T> where T : class, IRelationship
	{
		IRelationshipSet<T> GetBeginningWith(ITrueEntity begin);

		IRelationshipSet<T> GetEndingWith(ITrueEntity begin);

		ISet<ITrueEntity> GetBeginEntities();

		ISet<ITrueEntity> GetEndEntities();

		new IRelationshipSet<M> ConvertAll<M>(Converter<T, M> converter) where M : class, IRelationship;
	}
}
