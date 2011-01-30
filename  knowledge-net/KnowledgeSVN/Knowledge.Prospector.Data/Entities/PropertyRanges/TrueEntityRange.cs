using System;

namespace Knowledge.Prospector.Data.Entities
{
	[Serializable]
	public class TrueEntityRange : IRange
	{
		public TrueEntityRange(ITrueEntity trueEntity)
		{
			if (trueEntity == null)
				throw new NullReferenceException("trueEntity");
			_TrueEntity = trueEntity;
		}

		private ITrueEntity _TrueEntity;

		#region IRange Members

		public string Name
		{
			get { return _TrueEntity.Identity; }
		}

		#endregion
	}
}
