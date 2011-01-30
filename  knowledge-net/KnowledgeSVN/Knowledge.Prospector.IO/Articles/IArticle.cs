using System.Collections.Generic;
using Knowledge.Prospector.IO.Documents;

namespace Knowledge.Prospector.IO.Articles
{
	/// <summary>
	/// Article is collection of natural language words.
	/// </summary>
	/// <remarks>In future to this interface can be added additional information about article. Such as title, author, creation time etc.</remarks>
	public interface IArticle
	{
		/// <summary>
		/// Text contained in article.
		/// </summary>
		string Text { get; set;}

		/// <summary>
		/// Short name of article type.
		/// </summary>
		string ArticleType { get;}

		/// <summary>
		/// Document contained this article.
		/// </summary>
		IDocument SourceDocument { get;}

		ArticleInfo Info { get;}
	}
}
