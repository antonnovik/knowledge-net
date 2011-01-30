using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public class SubpropertyRelationship : Relationship
	{
		public SubpropertyRelationship(IPropertyEntity descendant, IPropertyEntity parent)
			: base(2)
		{
			base.Entities[0] = descendant;
			base.Entities[1] = parent;
			base.GenerateIdentity();
		}

		public IPropertyEntity Descendant
		{
			get { return base.Entities[0] as IPropertyEntity; }
			//set { base.Entities[0] = value; }
		}

		public IPropertyEntity Parent
		{
			get { return base.Entities[1] as IPropertyEntity; }
			//set { base.Entities[1] = value; }
		}

		#region IRelationship Members

		public override string Name
		{
			get { return "Subproperty"; }
		}

		#endregion
	}
}
