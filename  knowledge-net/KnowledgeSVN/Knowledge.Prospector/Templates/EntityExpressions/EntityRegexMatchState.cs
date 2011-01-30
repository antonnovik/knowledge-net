using System.Collections.Generic;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public class EntityRegexMatchState : List<StateItem>
	{
		public EntityRegexMatchState()
			: base()
		{
		}

		public EntityRegexMatchState(int count)
			: base(count)
		{
		}
		public EntityRegexMatchState Clone()
		{
			EntityRegexMatchState result = new EntityRegexMatchState(this.Count);

			foreach (StateItem item in this)
			{
				result.Add(new StateItem(item));
			}

			return result;
		}

		public EntityRegexMatchState Clone(int lastIndex)
		{
			EntityRegexMatchState result = new EntityRegexMatchState(this.Count);

			foreach (StateItem item in this)
			{
				if (item.LastPatternIndex == lastIndex)
					result.Add(new StateItem(item));
			}

			return result;
		}
	}
}
