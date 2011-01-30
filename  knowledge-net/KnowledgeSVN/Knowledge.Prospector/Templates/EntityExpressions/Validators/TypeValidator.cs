using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	public class TypeValidator : BaseValidator
	{
		private Type ValidType;

		public TypeValidator(Type validType)
		{
			ValidType = validType;
		}
		public override bool IsValid(IEntity entity)
		{
			if (entity is ChangeableEntity)
				return IsValid(entity as ChangeableEntity);
			return ValidType.IsInstanceOfType(entity);
		}

		public bool IsValid(ChangeableEntity changeableEntity)
		{
			foreach (IEntity allowedEntity in changeableEntity.AllowedEntities)
			{
				if (ValidType.IsInstanceOfType(allowedEntity))
				{
					changeableEntity.ChangeToEntity = allowedEntity;
					return true;
				}
			}
			return false;
		}

		public override string ToString()
		{
			return string.Format("TypeValidator: {0}", ValidType.Name);
		}
	}
}
