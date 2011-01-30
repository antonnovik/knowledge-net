using Knowledge.Prospector.Data.Entities;

namespace Knowledge.Prospector.Languages
{
	/// <summary>
	/// Language used for connecting translator to dictionaries.
	/// </summary>
	public interface ILanguage
	{
		/// <summary>
		/// Name of the language ("Russian", "English", ...)
		/// </summary>
		string Name { get;}

		/// <summary>
		/// Convert lexical element of natural language to Entity class
		/// </summary>
		/// <param name="word">Lexical element</param>
		/// <returns>IEntity what present element</returns>
		IEntity Translate(string word);

		/// <summary>
		/// Gets all dictionaries of current language from Dictionary manager.
		/// </summary>
		void ResetDictionaries();
	}
}