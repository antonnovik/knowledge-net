using Knowledge.Prospector.Data.Entities;
using System;

namespace Knowledge.Prospector.Data.Rules
{
	[Serializable]
	public class ConditionalRule
	{
		public ClassPropertyCondition IfCondition;
		public ClassPropertyCondition ThenCondition;
//		public ClassPropertyCondition ElseCondition;

		public ConditionalRule(IClassEntity classEntity, IPropertyEntity ifProperty, IPropertyEntity thenProperty)
		{
			IfCondition = new ClassPropertyCondition(classEntity, ifProperty);
			ThenCondition = new ClassPropertyCondition(classEntity, thenProperty);

		}
	}
}
