using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public static class RelationshipAdapter
	{
		public static IRelationship CreateRelationship(RelationshipEntity relationshipEntity)
		{
			switch (relationshipEntity.Type)
			{
				case "equivalence":
					return new EquivalenceRelationship(relationshipEntity);
				case "subclass":
					return new SubclassRelationship(relationshipEntity);
				default:
					return null;
			}
		}
	}
}
