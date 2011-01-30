using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public abstract class ValidatorExpression
	{
		public abstract bool Interpret(IEntity entity);
	}
}
