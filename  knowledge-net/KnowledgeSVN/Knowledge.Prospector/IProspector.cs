using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.IO.Articles;

namespace Knowledge.Prospector
{
	/// <summary>
	/// Used to prospect knowledge in article.
	/// </summary>
	public interface IProspector<TArticle> : IProspector
		where TArticle : IArticle
	{
		/// <summary>
		/// Prospect article for knowledge.
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		void Prospect(EntityGraph entityGraph, TArticle article);
	}

	public interface IProspector
	{
		/// <summary>
		/// Prospect article for knowledge.
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		void Prospect(EntityGraph entityGraph, IArticle article);
	}
}
