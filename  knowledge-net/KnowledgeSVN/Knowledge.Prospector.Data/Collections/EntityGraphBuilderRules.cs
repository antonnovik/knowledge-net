using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Collections
{
	/// <summary>
	/// Rules used by Entity Graph Builder.
	/// </summary>
	public static class EntityGraphBuilderRules
	{
		/// <summary>
		/// Add to IEntityGraph all instances of ITrueEntity from IEntityList.
		/// </summary>
		/// <param name="builder">IEntityGraph to with add entities.</param>
		/// <returns>New IEntityGraph.</returns>
		public static void UseEntitiesRule(IEntityGraphBuilder builder)
		{
			for (int i = 0; i < builder.Source.Count; i++)
			{
				if (builder.Source[i] is ITrueEntity)
					builder.Add(builder.Source[i] as ITrueEntity);
			}
		}

		/// <summary>
		/// Convert all IRelationshipEntity to corresponding IRelationship.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static void UseConvertRelationshipEntityToRelationshipRule(IEntityGraphBuilder builder)
		{
			foreach (IEntityList<IEntity> sentence in builder.Source.Split(new IEntity[] { SeparatorEntity.Dot, SeparatorEntity.Comma }))
			{
				IEntity[] relationshipEntities = sentence.FindAllByType(typeof(RelationshipEntity));
				if (relationshipEntities != null && relationshipEntities.Length == 1)
				{
					IEntity[] leftClassEntities = sentence.GetLeftPartFrom(relationshipEntities[0]).FindAllByType(typeof(IClassEntity));
					IEntity[] rightClassEntities = sentence.GetRightPartFrom(relationshipEntities[0]).FindAllByType(typeof(IClassEntity));
					if (leftClassEntities != null && leftClassEntities.Length == 1
						&& rightClassEntities != null && rightClassEntities.Length == 1)
					{
						//Add Relationship
						IRelationship relation = RelationshipAdapter.CreateRelationship(relationshipEntities[0] as RelationshipEntity);
						relation.Entities.Add(TrueEntity.ToTrueEntity(leftClassEntities[0]));
						relation.Entities.Add(TrueEntity.ToTrueEntity(rightClassEntities[0]));
						builder.Add(relation);
					}
				}
			}
		}
	}
}