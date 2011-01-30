namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public abstract class EntityExpression
	{
		public abstract EntityRegexMatchState Interpret(EntityRegexMatchInfo info, EntityRegexMatchState state);
	}
}
