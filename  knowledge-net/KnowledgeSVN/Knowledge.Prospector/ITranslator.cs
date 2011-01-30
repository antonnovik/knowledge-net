using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.IO.Articles;

namespace Knowledge.Prospector
{
	/// <summary>
	/// Used for translating from natural language article to entity article. Translator use dictionaries for this task.
	/// </summary>
	public interface ITranslator
	{
		/// <summary>
		/// Translate natural article to list of entities.
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		IEntityList<IEntity> Translate(IArticle article);
	}
}
