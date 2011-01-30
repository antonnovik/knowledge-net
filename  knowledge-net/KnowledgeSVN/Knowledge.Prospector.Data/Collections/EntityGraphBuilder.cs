using System;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Collections
{
	public class EntityGraphBuilder : IEntityGraphBuilder
	{
		public int _index;
		private EntityGraph Graph;
		private IEntityList<IEntity> _source;

		private bool AssertIndex(int index)
		{
			if (!(0 <= index && index < Source.Count))
				return false;
			else
				return true;
		}

		public EntityGraphBuilder(EntityGraph graph, IEntityList<IEntity> source)
		{
			Graph = graph;
			Source = source;
		}

		public int Index
		{
			get { return _index; }
			private set { _index = value; }
		}

		#region IEntityGraphBuilder Members

		public IEntityList<IEntity> Source
		{
			get { return _source; }
			private set { _source = value; }
		}

		public void Add(ITrueEntity trueEntity)
		{
			Graph.Add(trueEntity);			
		}

		public void Add(IClassEntity classEntity)
		{
			Graph.Add(classEntity);
		}

		public void Add(IPropertyEntity propertyEntity)
		{
			Graph.Add(propertyEntity);
		}

		public void Add(IIndividualEntity individualEntity)
		{
			Graph.Add(individualEntity);
		}

		public void Add(IDatatypeEntity datatypeEntity)
		{
			Graph.Add(datatypeEntity);
		}

		public void Add(IRelationship relationship)
		{
			Graph.Add(relationship);
		}

		public void Add(SubclassRelationship subclassRelationship)
		{
			Graph.Add(subclassRelationship);
		}

		public void Add(PropertyRelationship propertyRelationship)
		{
			Graph.Add(propertyRelationship);
		}

		public void Add(SubpropertyRelationship subpropertyRelationship)
		{
			Graph.Add(subpropertyRelationship);
		}

		public void Add(EquivalenceRelationship equivalenceRelationship)
		{
			Graph.Add(equivalenceRelationship);
		}

		public void Add(ConditionalRuleRelationship conditionalRuleRelationship)
		{
			Graph.Add(conditionalRuleRelationship);
		}


		public bool MoveNext()
		{
			if(++Index<Source.Count)
				return true;
			else
			{
				Index = Source.Count;
				return false;
			}
		}

		public bool Move(int to)
		{
			int newIndex = Index + to;
			if (AssertIndex(newIndex))
			{
				Index = newIndex;
				return true;
			}
			else
				return false;
		}

		public void Reset()
		{
			Index = -1;
		}

		public IEntity Current
		{
			get
			{
				return Source[Index];	
			}
		}

		public bool AssertCurrent
		{
			get
			{
				return AssertIndex(Index);
			}
		}

		public event SourceChanged OnChanged;

		#endregion

		#region IEntityGraphBuilder Members

		public void Insert(int position, PositionType positionType, IEntity entity, InsertionType insertionType)
		{
			int actualPosition = positionType == PositionType.Absolute ? position : Index + position;
			if (insertionType == InsertionType.Before) actualPosition--;
			if (AssertIndex(actualPosition))
				Source.Insert(actualPosition, entity);
			else
				throw new ArgumentOutOfRangeException("position");
			if (OnChanged != null)
				OnChanged();
			if (Index > actualPosition)
				Index++;
		}

		public void Delete(int position, PositionType positionType)
		{
			int actualPosition = positionType == PositionType.Absolute ? position : Index + position;
			if (AssertIndex(actualPosition))
				Source.RemoveAt(actualPosition);
			else
				throw new ArgumentOutOfRangeException("position");
			if (OnChanged != null)
				OnChanged();
			if (Index > actualPosition)
				Index--;
		}

		public void Replace(int position, PositionType positionType, IEntity newEntity)
		{
			int actualPosition = positionType == PositionType.Absolute ? position : Index + position;
			if (AssertIndex(actualPosition))
				Source[actualPosition] = newEntity;
			else
				throw new ArgumentOutOfRangeException("position");
			if (OnChanged != null)
				OnChanged();
		}

		public void Replace(int position, PositionType positionType, IEntity newEntity, int count)
		{
			int actualPosition = positionType == PositionType.Absolute ? position : Index + position;
			for(int i=0; i< count; i++)
				if(!AssertIndex(actualPosition + i))
					throw new ArgumentOutOfRangeException("position");

			Source[actualPosition] = newEntity;
			for(int i=1; i<count; i++)
				Source.RemoveAt(actualPosition + 1);

			if (OnChanged != null)
				OnChanged();
		}

		#endregion
	}
}
