using System;

namespace Knowledge.Prospector.Data.Entities
{
	[Serializable]
	public class IntegerRange : IRange
	{
		private IntegerRange()
		{ }

		public static readonly IntegerRange Instance = new IntegerRange();

		#region IRange Members

		public string Name
		{
			get { return "Integer"; }
		}

		#endregion
	}
}
