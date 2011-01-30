using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Collections
{
	public delegate void SourceChanged();

	public enum InsertionType
	{
		After,
		Before
	}

	public enum PositionType
	{
		Absolute,
		Relative
	}

	public interface IEntityGraphBuilder
	{
		/// <summary>
		/// Source text used for building graph.
		/// </summary>
		IEntityList<IEntity> Source { get;}

		/// <summary>
		/// Adds ITrueEntity to the graph.
		/// </summary>
		/// <param name="trueEntity"></param>
		void Add(ITrueEntity trueEntity);

		/// <summary>
		/// Adds IClassEntity to the graph.
		/// </summary>
		/// <param name="classEntity"></param>
		void Add(IClassEntity classEntity);

		/// <summary>
		/// Adds IPropertyEntity to the graph.
		/// </summary>
		/// <param name="propertyEntity"></param>
		void Add(IPropertyEntity propertyEntity);

		/// <summary>
		/// Adds IIndividualEntity to the graph.
		/// </summary>
		/// <param name="individualEntity"></param>
		void Add(IIndividualEntity individualEntity);

		/// <summary>
		/// Adds IDatatypeEntity to the graph.
		/// </summary>
		/// <param name="datatypeEntity"></param>
		void Add(IDatatypeEntity datatypeEntity);

		/// <summary>
		/// Adds IRelationship to the graph.
		/// </summary>
		/// <param name="relationship"></param>
		void Add(IRelationship relationship);

		/// <summary>
		/// Adds SubclassRelationship to the graph.
		/// </summary>
		/// <param name="subclassRelationship"></param>
		void Add(SubclassRelationship subclassRelationship);

		/// <summary>
		/// Adds PropertyRelationship to the graph.
		/// </summary>
		/// <param name="propertyRelationship"></param>
		void Add(PropertyRelationship propertyRelationship);

		/// <summary>
		/// Adds SubpropertyRelationship to the graph.
		/// </summary>
		/// <param name="subpropertyRelationship"></param>
		void Add(SubpropertyRelationship subpropertyRelationship);

		/// <summary>
		/// Adds EquivalenceRelationship to the graph.
		/// </summary>
		/// <param name="equivalenceRelationship"></param>
		void Add(EquivalenceRelationship equivalenceRelationship);

		void Add(ConditionalRuleRelationship conditionalRuleRelationship);

		/// <summary>
		/// Current index of iterator.
		/// </summary>
		int Index { get;}

		/// <summary>
		/// Move iterator to next entity.
		/// </summary>
		/// <returns></returns>
		bool MoveNext();

		/// <summary>
		/// Move iterator to several entities.
		/// +N = forward to N entities..
		/// -N back to N entities.
		/// </summary>
		/// <param name="to"></param>
		/// <returns></returns>
		bool Move(int to);

		/// <summary>
		/// Reset iterator.
		/// </summary>
		void Reset();

		/// <summary>
		/// Gets current iterator entity.
		/// </summary>
		IEntity Current { get;}

		/// <summary>
		/// Insert new IEntity to specified position.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="positionType"></param>
		/// <param name="entity"></param>
		/// <param name="insertionType"></param>
		void Insert(int position, PositionType positionType, IEntity entity, InsertionType insertionType);

		/// <summary>
		/// Delete entity at specified position.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="positionType"></param>
		void Delete(int position, PositionType positionType);

		/// <summary>
		/// Replace old entity to new at specified position.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="positionType"></param>
		/// <param name="newEntity"></param>
		void Replace(int position, PositionType positionType, IEntity newEntity);

		/// <summary>
		/// Replace count entities with newEntity.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="positionType"></param>
		/// <param name="newEntity"></param>
		/// <param name="count"></param>
		void Replace(int position, PositionType positionType, IEntity newEntity, int count);

		/// <summary>
		/// Fires whan source changed.
		/// </summary>
		event SourceChanged OnChanged;
	}
}
