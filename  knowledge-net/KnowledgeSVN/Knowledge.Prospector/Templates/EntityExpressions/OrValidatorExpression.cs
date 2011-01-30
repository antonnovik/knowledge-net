using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	class OrValidatorExpression : ValidatorExpression
	{
		private ValidatorExpression Left, Right;

		public override bool Interpret(IEntity entity)
		{
			return Left.Interpret(entity) || Right.Interpret(entity);
		}

		public OrValidatorExpression(string left, string right)
		{
			Left = ValidatorExpressionParser.Parse(left);
			Right = ValidatorExpressionParser.Parse(right);
		}
	}
}
