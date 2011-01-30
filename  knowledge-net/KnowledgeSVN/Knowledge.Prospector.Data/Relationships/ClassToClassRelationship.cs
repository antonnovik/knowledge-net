using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public class ClassToClassRelationship : Relationship
	{
		public ClassToClassRelationship(string value, IClassEntity left, IClassEntity right, RelationshipKind kind)
			: base(2)
		{
			base.Value = value;
			base.Entities[0] = left;
			base.Entities[1] = right;
			base.Kind = kind;
			base.GenerateIdentity();
		}

		public IClassEntity Left
		{
			get { return base.Entities[0] as IClassEntity; }
		}

		public IClassEntity Right
		{
			get { return base.Entities[1] as IClassEntity; }
		}

		#region IRelationship Members

		public override string Name
		{
			get { return "ClassToClass"; }
		}

		#endregion

	}
}
