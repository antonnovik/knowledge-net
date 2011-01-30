using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Class for property entity.
	/// </summary>
	/// <remarks>Properties can be used to state relationships between individuals or from individuals to data values.</remarks>
	[Serializable]
	public class PropertyEntity : TrueEntity, IPropertyEntity
	{
		private IRange _range;
		private IPropertyEntity _inverseOf;

		public PropertyEntity(string value)
			: base(value) { }

		#region IEntity Members

		public override string Name
		{
			get { return "Property"; }
		}

		#endregion

		/// <summary>
		/// Gets or sets range of property.
		/// </summary>
		public IRange Range
		{
			get
			{
				return _range;
			}
			set
			{
				_range = value;
			}
		}

		/// <summary>
		/// Gets or sets the property to with this one is inverse of.
		/// </summary>
		/// <remarks>
		/// If the property P1 is stated to be the inverse of the property P2, then if X is related to Y by the P2 property, then Y is related to X by the P1 property.
		/// If a property is inverse functional then the inverse of the property is functional. Thus the inverse of the property has at most one value for each individual.
		/// </remarks>
		public IPropertyEntity InverseOf
		{
			get
			{
				return _inverseOf;
			}
			set
			{
				_inverseOf = value; ;
			}
		}

		public override string ToString()
		{
			return string.Format("<Property>{0}</Property>", Value);
		}
	}
}
