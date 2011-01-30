using System.Collections.Generic;

namespace Knowledge.Prospector.Templates.EntityExpressions
{
	public class StateItem
	{
		/// <summary>
		/// Index = Number of checked Entity.
		/// Value = Number of pattern used for checking.
		/// </summary>
		public List<int> Passed;

		public StateItem()
		{
			Passed = new List<int>();
		}

		public StateItem(StateItem item)
		{
			Passed = new List<int>(item.Passed);
		}

		#region Working with addition information

		/// <summary>
		/// Addition information about passed entity.
		/// </summary>
		private List<IntToStringRelationship> EntitiesValues;

		private void SetEntityValue(int entityIndex, string value)
		{
			if (EntitiesValues == null)
				EntitiesValues = new List<IntToStringRelationship>();
			EntitiesValues.Add(new IntToStringRelationship(entityIndex, value));
		}

		public string GetEntityValue(int entityIndex)
		{
			if (EntitiesValues != null)
				foreach (IntToStringRelationship relat in EntitiesValues)
					if (relat.IntValue == entityIndex)
						return relat.StringValue;
			return null;
		}

		#endregion

		public int LastPatternIndex
		{
			get{return Passed.Count == 0 ? -1 : Passed[Passed.Count - 1];}
		}

		public int NextEntityIndex
		{
			get { return Passed.Count; }
		}

		public int CountOfPassedEntities
		{
			get { return Passed.Count; }
		}

		public void AddPassed(int patternIndex)
		{
			AddPassed(patternIndex, null);
		}

		public void AddPassed(int patternIndex, string value)
		{
			if (value != null)
				SetEntityValue(NextEntityIndex, value);
			Passed.Add(patternIndex);
		}

		public int GetFirstMatchedIndex(int patternIndex)
		{
			if (Passed[patternIndex] == patternIndex)
				return patternIndex;

			for (int i = 0; i < Passed.Count; i++)
				if (Passed[i] == patternIndex)
					return i;
			return -1;
		}
		/// <summary>
		/// Adds passed indexes to StateItem.
		/// </summary>
		/// <param name="patternIndex">Index of passed pattern.</param>
		/// <param name="subStateItem">Passed indexes in sub pattern.</param>
		/// <param name="passedIndex">Only this index will be added to passed.</param>
		public void AddPassed(int patternIndex, StateItem subStateItem, int passedIndex)
		{
			AddPassed(patternIndex, subStateItem, patternIndex, null);
		}

		/// <summary>
		/// Adds passed indexes to StateItem.
		/// </summary>
		/// <param name="patternIndex">Index of passed pattern.</param>
		/// <param name="subStateItem">Passed indexes in sub pattern.</param>
		/// <param name="passedIndex">Only this index will be added to passed.</param>
		public void AddPassed(int patternIndex, StateItem subStateItem, int passedIndex, string value)
		{
			for (int i = 0; i < subStateItem.CountOfPassedEntities; i++)
			{
				if (subStateItem.Passed[i] == passedIndex)
				{
					if (value != null)
						SetEntityValue(NextEntityIndex, value);
					Passed.Add(patternIndex);
				}
				else
					Passed.Add(-1);
			}
		}

		#region Static methods

		//public static List<StateItem> Clone(List<StateItem> list)
		//{
		//    List<StateItem> result = new List<StateItem>(list.Count);

		//    foreach (StateItem item in list)
		//    {
		//        result.Add(new StateItem(item));
		//    }

		//    return result;
		//}

		//public static List<StateItem> Clone(List<StateItem> list, int lastIndex)
		//{
		//    List<StateItem> result = new List<StateItem>(list.Count);

		//    foreach (StateItem item in list)
		//    {
		//        if(item.LastPatternIndex == lastIndex)
		//            result.Add(new StateItem(item));
		//    }

		//    return result;
		//}

		#endregion

		public override string ToString()
		{
			string temp = string.Empty;
			foreach (int b in Passed)
				temp += " " + b.ToString();

			return temp;
		}

		#region Nested class "IntToStringRelationship"
		private class IntToStringRelationship
		{
			public int IntValue;
			public string StringValue;

			public IntToStringRelationship(int IntValue, string StringValue)
			{
				this.IntValue = IntValue;
				this.StringValue = StringValue;
			}
		}
		#endregion
	}
}
