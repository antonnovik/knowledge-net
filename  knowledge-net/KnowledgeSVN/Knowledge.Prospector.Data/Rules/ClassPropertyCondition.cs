using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Rules
{
	[Serializable]
	public class ClassPropertyCondition
	{
		public IPropertyEntity PropertyEntity;
		public IClassEntity ClassEntity;

//		public bool IsTrue = false;

		public ClassPropertyCondition(IClassEntity classEntity, IPropertyEntity propertyEntity)
		{
			ClassEntity = classEntity;
			PropertyEntity = propertyEntity;
		}
	}
}
