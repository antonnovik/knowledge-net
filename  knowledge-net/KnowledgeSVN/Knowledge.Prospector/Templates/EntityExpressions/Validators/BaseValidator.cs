using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions.Validators
{
	public abstract class BaseValidator : ValidatorExpression
	{
		public abstract bool IsValid(IEntity entity);

		public override bool Interpret(IEntity entity)
		{
			return IsValid(entity);
		}
	}
}
