using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public class PropertyRelationship : Relationship
	{
		public PropertyRelationship(IPropertyEntity propertyEntity, IClassEntity classEntity)
			: base(2)
		{
			base.Entities[0] = propertyEntity;
			base.Entities[1] = classEntity;
			base.GenerateIdentity();
		}
				
		public IPropertyEntity Property
		{
			get { return base.Entities[0] as IPropertyEntity; }
		}

		public IClassEntity Class
		{
			get { return base.Entities[1] as IClassEntity; }
		}

		#region IRelationship Members

		public override string Name
		{
			get { return "Property"; }
		}

		#endregion

	}
}
