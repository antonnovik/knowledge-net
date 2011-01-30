using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public class EquivalenceRelationship : Relationship
	{
		public EquivalenceRelationship(RelationshipEntity relationshipEntity)
			: base(relationshipEntity) { }

		#region IRelationship Members

		public override string Name
		{
			get { return "Equivalence"; }
		}

		#endregion
	}
}
