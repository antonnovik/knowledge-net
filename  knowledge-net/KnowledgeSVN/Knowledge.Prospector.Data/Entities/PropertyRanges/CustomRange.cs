using System;

namespace Knowledge.Prospector.Data.Entities
{
	[Serializable]
	public class CustomRange : IRange
	{
		public CustomRange(string rangeName)
		{
			if (rangeName == null || rangeName.Trim() == string.Empty)
				throw new NullReferenceException("trueEntity");
			_RangeName = rangeName;
		}

		private string _RangeName;

		#region IRange Members

		public string Name
		{
			get { return _RangeName; }
		}

		#endregion
	}
}
