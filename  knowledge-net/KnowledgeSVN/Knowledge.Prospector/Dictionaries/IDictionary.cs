using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Languages;

namespace Knowledge.Prospector.Dictionaries
{
	/// <summary>
	/// Convert natural language word to entity.
	/// </summary>
	public interface IDictionary
	{
		/// <summary>
		/// Convert lexical element of natural language to Entity class.
		/// </summary>
		/// <param name="word">Lexical element.</param>
		/// <returns>IEntity what present element.</returns>
		IEntity Translate(string word);

		/// <summary>
		/// Returns all languages for what this dictionary be right.
		/// </summary>
		ILanguage[] Languages { get;}
	}
}
