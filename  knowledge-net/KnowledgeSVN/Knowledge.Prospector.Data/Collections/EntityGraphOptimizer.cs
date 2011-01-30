using System.Collections.Generic;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Collections
{
	public class EntityGraphOptimizer
	{
		private EntityGraph Graph;

		public EntityGraphOptimizer(EntityGraph graph)
		{
			Graph = graph;
		}


		private int RelationshipLength(IRelationshipSet<IRelationship> relationships, ITrueEntity begin, ITrueEntity end)
		{
			if (begin.Equals(end))
				return 0;

			int length = -1;
			foreach (IRelationship rel in relationships.GetBeginningWith(begin).GetItems())
			{
				int temp = RelationshipLength(relationships, rel.Entities[1], end);
				if (temp > length) length = temp;
			}

			return length >= 0 ? length + 1 : -1;
		}

		/// <summary>
		/// Removes relationship between classes releted by two or more another relationships.
		/// </summary>
		/// <param name="graph"></param>
		public void OptimizeSubclassRelationship()
		{
			List<SubclassRelationship> list = new List<SubclassRelationship>(Graph.SubclassRelationships.GetItems());
			IRelationshipSet<IRelationship> relationships = Graph.SubclassRelationships.ConvertAll<IRelationship>(delegate(SubclassRelationship sr) { return sr as IRelationship; });

			for (int i = 0; i < list.Count; i++)
			{
				if (RelationshipLength(relationships, list[i].Entities[0], list[i].Entities[1]) > 1)
				{
					list.RemoveAt(i--);
				}
			}

			IRelationshipSet<SubclassRelationship> set = new RelationshipSet<SubclassRelationship>();
			set.Add(list);
			Graph.SubclassRelationships = set;
		}

		public void OptimizePropertyRelationships()
		{
			//all optimized PropertyRelationships
			IRelationshipSet<PropertyRelationship> optimizedPRs = Graph.PropertyRelationships;

			//all SubclassRelationships
			IRelationshipSet<IRelationship> allSubclassR = Graph.SubclassRelationships.ConvertAll<IRelationship>(delegate(SubclassRelationship sr) { return sr as IRelationship; });

			//all participated IPropertyEntity
			ISet<ITrueEntity> properties = Graph.PropertyRelationships.GetBeginEntities();

			IRelationshipSet<PropertyRelationship> set = new RelationshipSet<PropertyRelationship>();
			//foreach participated property
			foreach(ITrueEntity property in properties.GetItems())
			{
				//find PropertyRelationships related with this property from optimized
				IRelationshipSet<PropertyRelationship> someOptimizedPR = optimizedPRs.GetBeginningWith(property);

				//find all related classes by optimized PropertyRelationships
				//ISet<ITrueEntity> classes = someOptimizedPR.GetEndEntities();
				List<PropertyRelationship> list = new List<PropertyRelationship>(someOptimizedPR.GetItems());
				for(int i =0; i< list.Count; i++)
					for (int j = 0; j < list.Count; j++)
					{
						if (RelationshipLength(allSubclassR, list[j].Entities[1], list[i].Entities[1]) > 0)
						{
							list.RemoveAt(j--);
							if (i > j) i--;
						}
					}
				set.Add(list);
			}

			Graph.PropertyRelationships = set;
		}
	}
}
