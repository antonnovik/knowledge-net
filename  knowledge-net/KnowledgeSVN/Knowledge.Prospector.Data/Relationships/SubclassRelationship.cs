using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public class SubclassRelationship : Relationship
	{
		public SubclassRelationship(RelationshipEntity relationshipEntity)
			: base(relationshipEntity) { }

		public SubclassRelationship(IClassEntity descendant, IClassEntity parent)
			: base(2)
		{
			base.Entities[0] = descendant;
			base.Entities[1] = parent;
			base.GenerateIdentity();
		}

		public IClassEntity Descendant
		{
			get { return base.Entities[0] as IClassEntity; }
		}

		public IClassEntity Parent
		{
			get { return base.Entities[1] as IClassEntity; }
		}

		#region IRelationship Members

		public override string Name
		{
			get { return "Subclass"; }
		}

		#endregion
	}
}
