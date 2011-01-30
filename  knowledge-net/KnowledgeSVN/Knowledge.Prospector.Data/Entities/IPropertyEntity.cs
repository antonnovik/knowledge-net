namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Interface for property entity.
	/// </summary>
	/// <remarks>
	/// Properties can be used to state relationships
	/// between individuals or from individuals to data values.
	/// </remarks>
	public interface IPropertyEntity : ITrueEntity
	{
		/// <summary>
		/// Gets or sets range of property.
		/// </summary>
		IRange Range
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the property to with this one is inverse of.
		/// </summary>
		/// <remarks>
		/// If the property P1 is stated to be the inverse of the property P2,
		/// then if X is related to Y by the P2 property, 
		/// then Y is related to X by the P1 property.
		/// 
		/// If a property is inverse functional,
		/// then the inverse of the property is functional.
		/// 
		/// Thus the inverse of the property
		/// has at most one value for each individual.
		/// </remarks>
		IPropertyEntity InverseOf
		{
			get;
			set;
		}
	}
}
