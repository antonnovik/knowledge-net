using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public class EntityRegex
	{
		public EntityExpression[] AllRegex;

#if DEBUG
		string _pattern = string.Empty;
		public override string ToString()
		{
			return _pattern;
		}
#endif

		public EntityRegex(string pattern)
		{
#if DEBUG
			_pattern = pattern;
#endif
			AllRegex = EntityExpressionParser.Parse(pattern);
		}
		public EntityRegexMatchInfo Interpret(IEntity[] entities)
		{
			return Interpret(entities, 0);	
		}

		public EntityRegexMatchInfo Interpret(IEntity[] entities, int startIndex)
		{
			EntityRegexMatchInfo info = new EntityRegexMatchInfo(startIndex, this, entities);
			EntityRegexMatchState state = new EntityRegexMatchState();
			state.Add(new StateItem());

			for (int i = 0; i < AllRegex.Length; i++)
			{
				info.CurrentPatternIndex = i;
				state = AllRegex[i].Interpret(info, state);
			}

			info.States = state;
#if DEBUG
			if (state != null && state.Count > 1)
			{
				throw new Exception("Multi state situation arise!");
			}
#endif

			return info;
		}
	}
}
