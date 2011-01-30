using System;

namespace Knowledge.Prospector.Data.Relationships
{
	/// <summary>
	/// Relationship kinds.
	/// </summary>
	[Flags]
	public enum RelationshipKind
	{
		/// <summary>
		/// Transitive relationship.
		/// </summary>
		/// <remarks>
		/// If a property is transitive,
		/// then if the pair (x,y) is an instance of the transitive property P,
		/// and the pair (y,z) is an instance of P,
		/// then the pair (x,z) is also an instance of P.
		/// </remarks>
		Transitive = 1 << 0,

		/// <summary>
		/// Symmetric relationship.
		/// </summary>
		/// <remarks>
		/// If a property is symmetric,
		/// then if the pair (x,y) is an instance of the symmetric property P, 
		/// then the pair (y,x) is also an instance of P.
		/// </remarks>
		Symmetric = 1 << 1,

		/// <summary>
		/// Functional relationship.
		/// </summary>
		/// <remarks>
		/// If a property is functional, 
		/// then if the pair (x,y) is an instance of the functional property P, 
		/// and the pair (x,z) is an instance of P, then the y is equal to z.
		/// </remarks>
		Functional = 1 << 2,
	}

	public static class RelationshipKindHelper
	{
		public static RelationshipKind GetKind(string kind)
		{
			return (RelationshipKind)Enum.Parse(typeof(RelationshipKind), kind, true);
		}
	}
}