using System.Collections.Generic;
using Knowledge.Prospector.IO.Articles;

namespace Knowledge.Prospector.IO.Documents
{
	/// <summary>
	/// Document represent something what is finded by document provider.
	/// </summary>
	/// <remarks>Examples of IDocument can be HTML document, Text document, Doc document,  PDF document etc. In future to this interface can be added additional information about article. Such as title, author, creation time etc.</remarks>
	public interface IDocument : IEnumerable<IArticle>
	{
		void Close();

		DocumentInfo Info { get;set;}
	}
}
