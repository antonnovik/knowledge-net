using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Collections
{
	/// <summary>
	/// Entity graph used for storing knowledges obtained from article.
	/// </summary>
	[Serializable]
	public class EntityGraph
	{

		public EntityGraph()
		{
			_relationships.OnAdd += new SetEvent<IRelationship>(_relationships_OnAdd);
			_subclassRelationships.OnAdd += new SetEvent<SubclassRelationship>(_subclassRelationships_OnAdd);
			_propertyRelationships.OnAdd += new SetEvent<PropertyRelationship>(_propertyRelationships_OnAdd);
			_equivalenceRelationships.OnAdd += new SetEvent<EquivalenceRelationship>(_equivalenceRelationships_OnAdd);
			_ConditionalRuleRelationships.OnAdd += new SetEvent<ConditionalRuleRelationship>(_ConditionalRuleRelationships_OnAdd);
		}

		public void Dump(string fileName)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, this);
			stream.Close();
		}

		public static EntityGraph LoadDump(string fileName)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			EntityGraph result = (EntityGraph)formatter.Deserialize(stream);
			stream.Close();
			return result;
		}

		bool _relationships_OnAdd(IRelationship item)
		{
			return BeforeAddNewRelationship(item);
		}

		bool _equivalenceRelationships_OnAdd(EquivalenceRelationship item)
		{
			return BeforeAddNewRelationship(item);
		}

		bool _propertyRelationships_OnAdd(PropertyRelationship item)
		{
			return BeforeAddNewRelationship(item);
		}

		bool _subclassRelationships_OnAdd(SubclassRelationship item)
		{
			return BeforeAddNewRelationship(item);
		}

		bool _ConditionalRuleRelationships_OnAdd(ConditionalRuleRelationship item)
		{
			return BeforeAddNewRelationship(item);
		}


		/// <summary>
		/// Adds the relationship to the graph. Also adds the entities from the relationship to the graph.
		/// Execute IRelationship.GenerateId() before adding.
		/// </summary>
		/// <param name="item">The relationship to add to the IEntityGraph.</param>
		bool BeforeAddNewRelationship(IRelationship item)
		{
			if (item == null
				|| item.Entities == null
				|| item.Entities.Count == 0)
				return false;
			foreach (ITrueEntity entity in item.Entities)
				Add(entity);
			return true;
		}

		#region Properties

		#region Entities

		IEntitySet<IClassEntity> _classes = new EntitySet<IClassEntity>();
		IEntitySet<IPropertyEntity> _properties = new EntitySet<IPropertyEntity>();
		IEntitySet<IIndividualEntity> _individuals = new EntitySet<IIndividualEntity>();
		IEntitySet<IDatatypeEntity> _datatypes = new EntitySet<IDatatypeEntity>();

		/// <summary>
		/// Gets set of all IClassEntity entities contained in the graph.
		/// </summary>
		public IEntitySet<IClassEntity> Classes
		{
			get
			{
				return _classes;
			}
		}

		/// <summary>
		/// Gets set of all IPropertyEntity entities contained in the graph.
		/// </summary>
		public IEntitySet<IPropertyEntity> Properties
		{
			get
			{
				return _properties;
			}
		}

		/// <summary>
		/// Gets set of all IIndividualEntity entities contained in the graph.
		/// </summary>
		public IEntitySet<IIndividualEntity> Individuals
		{
			get
			{
				return _individuals;
			}
		}

		/// <summary>
		/// Gets set of all IDatatypeEntity entities contained in the graph.
		/// </summary>
		public IEntitySet<IDatatypeEntity> Datatypes
		{
			get
			{
				return _datatypes;
			}
		}

		#endregion

		#region Relationships

		IRelationshipSet<IRelationship> _relationships = new RelationshipSet<IRelationship>();
		IRelationshipSet<SubclassRelationship> _subclassRelationships = new RelationshipSet<SubclassRelationship>();
		IRelationshipSet<PropertyRelationship> _propertyRelationships = new RelationshipSet<PropertyRelationship>();
		IRelationshipSet<SubpropertyRelationship> _subpropertyRelationships = new RelationshipSet<SubpropertyRelationship>();
		IRelationshipSet<EquivalenceRelationship> _equivalenceRelationships = new RelationshipSet<EquivalenceRelationship>();
		IRelationshipSet<ConditionalRuleRelationship> _ConditionalRuleRelationships = new RelationshipSet<ConditionalRuleRelationship>();

		public IRelationshipSet<IRelationship> Relationships
		{
			get
			{
				return _relationships;
			}
		}

		/// <summary>
		/// Gets all SubclassRelationship contained in the graph.
		/// </summary>
		public IRelationshipSet<SubclassRelationship> SubclassRelationships
		{
			get
			{
				return _subclassRelationships;
			}
			set
			{
				_subclassRelationships = value;
			}
		}

		/// <summary>
		/// Gets all PropertyRelationship contained in the graph.
		/// </summary>
		public IRelationshipSet<PropertyRelationship> PropertyRelationships
		{
			get
			{
				return _propertyRelationships;
			}
			set
			{
				_propertyRelationships = value;
			}
		}

		/// <summary>
		/// Gets all SubpropertyRelationship contained in the graph.
		/// </summary>
		public IRelationshipSet<SubpropertyRelationship> SubpropertyRelationships
		{
			get
			{
				return _subpropertyRelationships;
			}
		}

		/// <summary>
		/// Gets all EquivalenceRelationship contained in the graph.
		/// </summary>
		public IRelationshipSet<EquivalenceRelationship> EquivalenceRelationships
		{
			get
			{
				return _equivalenceRelationships;
			}
		}

		/// <summary>
		/// Gets all ConditionalRuleRelationship contained in the graph.
		/// </summary>
		public IRelationshipSet<ConditionalRuleRelationship> ConditionalRuleRelationships
		{
			get { return _ConditionalRuleRelationships; }
		}

		#endregion

		#endregion

		#region Add functions

		/// <summary>
		/// Adds ITrueEntity to the graph.
		/// </summary>
		/// <param name="trueEntity"></param>
		public void Add(ITrueEntity trueEntity)
		{
			if (trueEntity is IClassEntity)
				Add(trueEntity as IClassEntity);
			else if (trueEntity is IPropertyEntity)
				Add(trueEntity as IPropertyEntity);
			else if (trueEntity is IIndividualEntity)
				Add(trueEntity as IIndividualEntity);
			else if (trueEntity is IDatatypeEntity)
				Add(trueEntity as IDatatypeEntity);
			else throw new ArgumentException("Unsopported subtype of ITrueEntity : " + trueEntity.GetType(), "trueEntity");
		}

		/// <summary>
		/// Adds IClassEntity to the graph.
		/// </summary>
		/// <param name="classEntity"></param>
		public void Add(IClassEntity classEntity)
		{
			Classes.Add(classEntity);
		}

		/// <summary>
		/// Adds IPropertyEntity to the graph.
		/// </summary>
		/// <param name="propertyEntity"></param>
		public void Add(IPropertyEntity propertyEntity)
		{
			Properties.Add(propertyEntity);
		}

		/// <summary>
		/// Adds IIndividualEntity to the graph.
		/// </summary>
		/// <param name="individualEntity"></param>
		public void Add(IIndividualEntity individualEntity)
		{
			Individuals.Add(individualEntity);
		}

		/// <summary>
		/// Adds IDatatypeEntity to the graph.
		/// </summary>
		/// <param name="datatypeEntity"></param>
		public void Add(IDatatypeEntity datatypeEntity)
		{
			Datatypes.Add(datatypeEntity);
		}

		/// <summary>
		/// Adds IRelationship to the graph.
		/// </summary>
		/// <param name="relationship"></param>
		public void Add(IRelationship relationship)
		{
			//if (relationship is SubclassRelationship)
			//    Add(relationship as SubclassRelationship);
			//else if (relationship is PropertyRelationship)
			//    Add(relationship as PropertyRelationship);
			//else if (relationship is EquivalenceRelationship)
			//    Add(relationship as EquivalenceRelationship);
			//else
				Relationships.Add(relationship);
		}

		/// <summary>
		/// Adds SubclassRelationship to the graph.
		/// </summary>
		/// <param name="subclassRelationship"></param>
		public void Add(SubclassRelationship subclassRelationship)
		{
			SubclassRelationships.Add(subclassRelationship);
		}

		/// <summary>
		/// Adds PropertyRelationship to the graph.
		/// </summary>
		/// <param name="propertyRelationship"></param>
		public void Add(PropertyRelationship propertyRelationship)
		{
			PropertyRelationships.Add(propertyRelationship);
		}

		/// <summary>
		/// Adds SubpropertyRelationship to the graph.
		/// </summary>
		/// <param name="subpropertyRelationship"></param>
		public void Add(SubpropertyRelationship subpropertyRelationship)
		{
			SubpropertyRelationships.Add(subpropertyRelationship);
		}

		/// <summary>
		/// Adds EquivalenceRelationship to the graph.
		/// </summary>
		/// <param name="equivalenceRelationship"></param>
		public void Add(EquivalenceRelationship equivalenceRelationship)
		{
			EquivalenceRelationships.Add(equivalenceRelationship);
		}

		public void Add(ConditionalRuleRelationship conditionalRuleRelationship)
		{
			ConditionalRuleRelationships.Add(conditionalRuleRelationship);
		}

		#endregion

		#region Get.. functions

		/// <summary>
		/// Gets set of all ITrueEntity entities contained in the graph.
		/// </summary>
		public IEntitySet<ITrueEntity> GetAllEntities()
		{
			IEntitySet<ITrueEntity> all = new EntitySet<ITrueEntity>();
			all.Add<IClassEntity>(Classes);
			all.Add<IPropertyEntity>(Properties);
			all.Add<IIndividualEntity>(Individuals);
			all.Add<IDatatypeEntity>(Datatypes);
			return all;
		}

		/// <summary>
		/// Gets all IRelationship contained in the graph.
		/// </summary>
		public IRelationship[] GetAllRelationships()
		{
			int size =
				Relationships.Count +
				SubclassRelationships.Count +
				PropertyRelationships.Count +
				EquivalenceRelationships.Count;

			List<IRelationship> all = new List<IRelationship>(size);
			foreach (Relationship r in Relationships.GetItems())
				all.Add(r);
			foreach (SubclassRelationship sr in SubclassRelationships.GetItems())
				all.Add(sr);
			foreach (PropertyRelationship pr in PropertyRelationships.GetItems())
				all.Add(pr);
			foreach (EquivalenceRelationship er in EquivalenceRelationships.GetItems())
				all.Add(er);
			return all.ToArray();
		}

		/// <summary>
		/// Returns all parent classes of class entity from graph.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>All parent classes.</returns>
		public IEntitySet<IClassEntity> GetParents(IClassEntity entity)
		{
			IEntitySet<IClassEntity> temp = new EntitySet<IClassEntity>();
			foreach (SubclassRelationship sr in SubclassRelationships.GetItems())
			{
				if (sr.Descendant.Identity == entity.Identity)
					temp.Add(sr.Parent);
			}
			return temp;
		}

		/// <summary>
		/// Returns all parent properties of property entity from graph.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>All parent properties.</returns>
		public IEntitySet<IPropertyEntity> GetParents(IPropertyEntity entity)
		{
			return new EntitySet<IPropertyEntity>();
		}

		/// <summary>
		/// Returns all properties from graph related with classEntity by PropertyRelationship.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public IEntitySet<IPropertyEntity> GetProperties(IClassEntity entity)
		{
			IEntitySet<IPropertyEntity> temp = new EntitySet<IPropertyEntity>();
			foreach (PropertyRelationship pr in PropertyRelationships.GetItems())
			{
				if (pr.Class.Identity == entity.Identity)
					temp.Add(pr.Property);
			}
			return temp;
		}

		/// <summary>
		/// Returns all classes from graph related with propertyEntity by PropertyRelationship.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public IEntitySet<IClassEntity> GetClasses(IPropertyEntity entity)
		{
			IEntitySet<IClassEntity> temp = new EntitySet<IClassEntity>();
			foreach (PropertyRelationship pr in PropertyRelationships.GetItems())
			{
				if (pr.Property.Identity == entity.Identity)
					temp.Add(pr.Class);
			}
			return temp;
		}

		#endregion
}
}
