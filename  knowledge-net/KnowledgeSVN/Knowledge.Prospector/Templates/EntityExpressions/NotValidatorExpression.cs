using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	class NotValidatorExpression : ValidatorExpression
	{
		private ValidatorExpression Expression;

		public override bool Interpret(IEntity entity)
		{
			return !Expression.Interpret(entity);
		}

		public NotValidatorExpression(string expression)
		{
			Expression = ValidatorExpressionParser.Parse(expression);
		}
	}
}
