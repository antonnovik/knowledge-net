using Knowledge.Prospector.Common;
using Knowledge.Prospector.Morphology;
using Knowledge.Prospector.Morphology.Grammar;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Base interface for all entities.
	/// </summary>
	public interface IEntity : IIdentityUser
	{
		/// <summary>
		/// Gets or sets name of entity.
		/// </summary>
		string Name { get;}

		/// <summary>
		/// Gets or sets natural word what entity contain.
		/// </summary>
		string Value { get; set;}

		/// <summary>
		/// Assigned part of speech.
		/// </summary>
		PartOfSpeech PartOfSpeech { get; set;}

		/// <summary>
		/// Gets or sets full morphological information about this entity.
		/// </summary>
		FullLemmaInfo MorphologicalInfo { get; set;}
	}
}
