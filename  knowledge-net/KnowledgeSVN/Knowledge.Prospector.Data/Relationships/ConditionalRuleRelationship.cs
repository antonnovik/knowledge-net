using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	[Serializable]
	public class ConditionalRuleRelationship : Relationship
	{
		public ConditionalRuleRelationship(IClassEntity classEntity, IPropertyEntity ifProperty, IPropertyEntity thenProperty)
			: base(3)
		{
			base.Entities[0] = classEntity;
			base.Entities[1] = ifProperty;
			base.Entities[2] = thenProperty;
			base.GenerateIdentity();
		}
				
		public IClassEntity Class
		{
			get { return base.Entities[0] as IClassEntity; }
		}

		public IPropertyEntity IfProperty
		{
			get { return base.Entities[1] as IPropertyEntity; }
		}

		public IPropertyEntity ThenProperty
		{
			get { return base.Entities[2] as IPropertyEntity; }
		}

		#region IRelationship Members

		public override string Name
		{
			get { return "ConditionalRule"; }
		}

		#endregion

	}
}


