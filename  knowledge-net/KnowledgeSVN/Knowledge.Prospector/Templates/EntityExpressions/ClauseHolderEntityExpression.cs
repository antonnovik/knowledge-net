using System;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	class ClauseHolderEntityExpression : EntityExpression
	{
		public ClauseHolder Holder;

		public override EntityRegexMatchState Interpret(EntityRegexMatchInfo info, EntityRegexMatchState state)
		{
			if (state == null || state.Count == 0)
				return null;

			EntityRegexMatchState result = new EntityRegexMatchState();
			//foeach Regex (ClauseHolder item) in ClauseHolder
			foreach (ClauseHolder.ClauseItem clauseHolderItem in Holder.Items)
			{
				//foreach stateItem in original state
				foreach (StateItem stateItem in state)
				{
					if (info.StartIndex + stateItem.NextEntityIndex < info.Entities.Length)
					{
						//evaluate regex
						EntityRegexMatchInfo temp = clauseHolderItem.Regex.Interpret(info.Entities, info.StartIndex + stateItem.NextEntityIndex);
						if (temp.States != null)
							//foreach stateItem in evaluated result
							foreach (StateItem subStateItem in temp.States)
							{
								//new state item
								StateItem tempStateItem = new StateItem(stateItem);
								//item passed for current pattern index, subStateItem only clauseHolderItem.Index indexes.
								tempStateItem.AddPassed(info.CurrentPatternIndex, subStateItem, clauseHolderItem.Index, clauseHolderItem.Value);
								result.Add(tempStateItem);
							}
					}
				}
			}
			return result;
		}

		public ClauseHolderEntityExpression(string pattern)
		{
			pattern = pattern.Trim().ToUpper();
			if (pattern == string.Empty)
				throw new ArgumentException("Cannot create ClauseHolderEntityExpression for empty string", "pattern");
			string first = pattern.Substring(1, pattern.IndexOf('.') -1 );
			string second = pattern.Substring(pattern.IndexOf('.') + 1);
			if(first != "H")
				throw new ArgumentException("pattern should be H character", "pattern");
			Holder = ClauseHolderManager.Instance[second];
		}
	}
}
