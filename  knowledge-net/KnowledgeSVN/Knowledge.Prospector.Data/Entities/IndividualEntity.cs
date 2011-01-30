using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Interface for individual entity.
	/// </summary>
	/// <remarks>Individuals are instances of classes, and properties may be used to relate one individual to another.</remarks>
	[Serializable]
	public class IndividualEntity : TrueEntity, IIndividualEntity
	{
		public IndividualEntity(string value)
			: base(value) { }

		#region IEntityBase Members

		public override string Name
		{
			get { return "Individual"; }
		}

		#endregion
	}
}
