using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	public class TypeValidatorFactory : ValidatorFactory<TypeValidator>
	{
		private static readonly TypeValidatorFactory _Instance = new TypeValidatorFactory();

		public static TypeValidatorFactory Instance
		{
			get { return _Instance; }
		}

		protected override TypeValidator ParseValidatorKey(string key)
		{
			switch (key)
			{
				case "P":
					return new TypeValidator(typeof(PropertyEntity));
				case "C":
					return new TypeValidator(typeof(ClassEntity));
				case "S":
					return new TypeValidator(typeof(SeparatorEntity));
				case "I":
					return new TypeValidator(typeof(IndividualEntity));
				case "U":
					return new TypeValidator(typeof(UnknownEntity));
				case "E":
					return new TypeValidator(typeof(Entity));
				case "INT":
					return new TypeValidator(typeof(IntegerEntity));
				case "DT":
				case "DATETIME":
					return new TypeValidator(typeof(DateTimeEntity));
				default:
					throw new Exception("Unsupported entity type \"" + key + "\".");
			}
		}
	}
}
