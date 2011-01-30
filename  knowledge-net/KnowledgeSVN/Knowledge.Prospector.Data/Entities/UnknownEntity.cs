using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Unrecognized entity.
	/// </summary>
	[Serializable]
	public class UnknownEntity : Entity
	{
		public UnknownEntity(string value)
			: base(value) { }

		#region IEntityBase Members

		public override string Name
		{
			get { return "Unknown"; }
		}

		#endregion

		public override string ToString()
		{
			return string.Format("<Unknown>{0}</Unknown>", Value);
		}
	}
}
