using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public class OneEntityExpression : EntityExpression
	{
		private ValidatorExpression Validator;

		public OneEntityExpression(string pattern)
		{
			pattern = Splitter.TrimBracket(pattern, '[', ']');
			Validator = ValidatorExpressionParser.Parse(pattern);
		}

		public override EntityRegexMatchState Interpret(EntityRegexMatchInfo info, EntityRegexMatchState state)
		{
			if (state == null || state.Count == 0)
				return null;

			EntityRegexMatchState result = new EntityRegexMatchState();

			foreach (StateItem item in state)
			{
				if (info.StartIndex + item.NextEntityIndex < info.Entities.Length && Validator.Interpret(info.GetNextEntity(item)))
				{
					item.AddPassed(info.CurrentPatternIndex);
					result.Add(item);
				}
			}

			return result;
		}

		public override string ToString()
		{
			return string.Format("OneEntityExpression({0})", Validator.ToString());
		}
	}
}
