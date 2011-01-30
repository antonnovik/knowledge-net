using Knowledge.Prospector.Data.Relationships;

namespace Knowledge.Prospector.Data.Entities
{
	public interface IRelationshipEntity : IEntity
	{
		/// <summary>
		/// Gets or sets type of property.
		/// </summary>
		RelationshipKind Kind { get; set;}

		string Type { get;set;}
	}
}
