using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public class OptionalEntityExpression : EntityExpression
	{
		private ValidatorExpression Validator;

		public OptionalEntityExpression(string pattern)
		{
			pattern = Splitter.TrimBracket(pattern, '[', ']');
			Validator = ValidatorExpressionParser.Parse(pattern);
		}

		public override EntityRegexMatchState Interpret(EntityRegexMatchInfo info, EntityRegexMatchState state)
		{
			if (state == null || state.Count == 0)
				return null;

			EntityRegexMatchState result = state.Clone();

			foreach (StateItem item in state)
			{
				if (info.NextEntityExists(item) && Validator.Interpret(info.GetNextEntity(item)))
				{
					item.AddPassed(info.CurrentPatternIndex);
					result.Add(item);
				}
			}

			return result;
		}
	}
}
