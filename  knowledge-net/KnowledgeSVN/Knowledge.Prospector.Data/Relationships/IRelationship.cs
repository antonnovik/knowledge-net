using Knowledge.Prospector.Common;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Data.Relationships
{
	/// <summary>
	/// Relationship between entities.
	/// </summary>
	public interface IRelationship : IIdentityUser
	{
		/// <summary>
		/// Name of relationship.
		/// </summary>
		string Name { get;}

		/// <summary>
		/// Gets or sets natural word what entity contain.
		/// </summary>
		string Value { get; set;}

		/// <summary>
		/// Gets or sets kinds of relationship.
		/// </summary>
		RelationshipKind Kind { get; set;}

		/// <summary>
		/// Entities participate in relationship.
		/// </summary>
		IEntityList<ITrueEntity> Entities { get;}

		RelationshipAttributes Attributes { get;set;}
	}
}
