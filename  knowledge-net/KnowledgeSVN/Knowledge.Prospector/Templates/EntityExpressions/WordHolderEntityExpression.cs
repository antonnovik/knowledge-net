using System;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public class WordHolderEntityExpression : EntityExpression
	{
		public WordHolder Holder;

		public override EntityRegexMatchState Interpret(EntityRegexMatchInfo info, EntityRegexMatchState state)
		{
			if (state == null || state.Count == 0)
				return null;

			EntityRegexMatchState result = new EntityRegexMatchState();

			//foreach stateItem in original state
			foreach (StateItem stateItem in state)
			{
				if (info.StartIndex + stateItem.NextEntityIndex < info.Entities.Length)
				{
					//evaluate regex
					IEntity entity = info.Entities[info.StartIndex + stateItem.NextEntityIndex];
					ChangeableEntity changeableEntity = entity as ChangeableEntity;
					WordHolder.WordItem item = null;
					if (changeableEntity != null)
					{
						foreach (IEntity allowedEntity in changeableEntity.AllowedEntities)
						{
							item = Holder.Find(allowedEntity.Value.ToUpper());
							if (item != null)
							{
								break;
							}
						}

					}
					else
					{
						item = Holder.Find(entity.Value.ToUpper());
					}

					if (item != null)
					{
						stateItem.AddPassed(info.CurrentPatternIndex, item.Value);
						result.Add(stateItem);
					}
				}
			}
			return result;
		}

		public WordHolderEntityExpression(string pattern)
		{
			pattern = pattern.Trim().ToUpper();
			if (pattern == string.Empty)
				throw new ArgumentException("Cannot create ClauseHolderEntityExpression for empty string", "pattern");
			string first = pattern.Substring(1, pattern.IndexOf('.') -1 );
			string second = pattern.Substring(pattern.IndexOf('.') + 1);
			if(first != "W")
				throw new ArgumentException("pattern should be W character", "pattern");
			Holder = WordHolderManager.Instance[second];
		}
	}
}
