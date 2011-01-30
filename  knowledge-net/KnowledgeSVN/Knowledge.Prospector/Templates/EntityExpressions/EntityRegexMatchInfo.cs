using System.Collections.Generic;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	/// <summary>
	/// NOTE! Must be changed to give more convinien interface.
	/// </summary>
	public class EntityRegexMatchInfo
	{
		private EntityRegex ParentRegex;
		public readonly int StartIndex;
		public EntityRegexMatchState States;
		public IEntity[] Entities;
		public int CurrentPatternIndex = 0;

		public EntityRegexMatchInfo(int startIndex, EntityRegex regex, IEntity[] entities)
		{
			StartIndex = startIndex;
			ParentRegex = regex;
			Entities = entities;
		}

		public List<IEntity>[] GetMatch()
		{
			List<IEntity>[] result = new List<IEntity>[ParentRegex.AllRegex.Length];

			StateItem item = States[0];

			for (int entityIndex = 0; entityIndex < item.Passed.Count; entityIndex++)
			{
				int patternIndex = item.Passed[entityIndex];
				if (patternIndex == -1)
					continue;
				if (result[patternIndex] == null)
					result[patternIndex] = new List<IEntity>();

				result[patternIndex].Add(Entities[StartIndex + entityIndex]);
			}
			return result;
		}

		public string GetFirstValue(int patternIndex)
		{
			StateItem item = States[0];
			int index = GetFirstMatchedIndex(patternIndex);
			string value = item.GetEntityValue(index - StartIndex);
			if (value == null)
				value = Entities[index].Value;
			return value;
		}

		public int GetFirstMatchedIndex(int patternIndex)
		{
			int stateItemFirstMatchedIndex = States[0].GetFirstMatchedIndex(patternIndex);
			return stateItemFirstMatchedIndex == -1 ? -1 : StartIndex + stateItemFirstMatchedIndex;
		}

		public IEntity GetNextEntity(StateItem stateItem)
		{
			return Entities[StartIndex + stateItem.NextEntityIndex];
		}

		public bool NextEntityExists(StateItem stateItem)
		{
			return StartIndex + stateItem.NextEntityIndex < Entities.Length;
		}
	}
}
