using Knowledge.Prospector.Common;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public class MultipleEntityExpression : EntityExpression
	{
		private ValidatorExpression Validator;

		public MultipleEntityExpression(string pattern)
		{
			pattern = Splitter.TrimBracket(pattern, '[', ']');
			Validator = ValidatorExpressionParser.Parse(pattern);
		}

		public override EntityRegexMatchState Interpret(EntityRegexMatchInfo info, EntityRegexMatchState state)
		{
			if (state == null || state.Count == 0)
				return null;

			EntityRegexMatchState result = state.Clone();

			EntityRegexMatchState temp = new EntityRegexMatchState();

			foreach (StateItem item in state)
			{
				if (Validator.Interpret(info.GetNextEntity(item)))
				{
					item.AddPassed(info.CurrentPatternIndex);
					temp.Add(item);
				}
			}

			temp = this.Interpret(info, temp);
			if (temp != null)
				result.AddRange(temp);

			return result;
		}
	}
}
