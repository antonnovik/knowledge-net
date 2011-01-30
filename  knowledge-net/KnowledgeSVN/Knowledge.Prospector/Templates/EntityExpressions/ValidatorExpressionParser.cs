using System;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Templates.EntityExpressions.Validators;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public static class ValidatorExpressionParser
	{
		public static ValidatorExpression Parse(string expression)
		{
			expression = Splitter.TrimBracket(expression, '(', ')');
			if (expression == null || expression == string.Empty)
				throw new ArgumentException(string.Format("Incorrect expression: {0}", expression));

			if (expression[0] == '^')
				return new NotValidatorExpression(expression.Substring(1));

			return SubParse(expression);
		}

		/// <summary>
		/// Parse string what contain logic.
		/// </summary>
		/// <param name="logic"></param>
		private static ValidatorExpression SubParse(string expression)
		{
			string left, right;
			if (Splitter.Split(expression, '(', ')', '\\', '&', out left, out right))
				return new AndValidatorExpression(left, right);

			if (Splitter.Split(expression, '(', ')', '\\', '|', out left, out right))
				return new OrValidatorExpression(left, right);

			if (Splitter.Split(expression, '(', ')', '\\', ' ', out left, out right))
				return new OrValidatorExpression(left, right);

			return ValidatorManager.Instance.GetValidator(expression);
		}
	}
}
