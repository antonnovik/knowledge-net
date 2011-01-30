using System.Collections.Generic;
using System.Xml;

namespace Knowledge.Prospector.IO.Documents.Providers
{
	/// <summary>
	/// Provider used for searching documents.
	/// </summary>
	/// <remarks>Examples of IDocumentProvider should be Internet, LocalComputerDocumentProvider.</remarks>
	public interface IDocumentProvider : IEnumerable<IDocument>
	{
		/// <summary>
		/// Initialize provider with configuration ( NOW only string )
		/// </summary>
		/// <param name="config">Configuration string</param>
		void Init(string config);

		void Close();
	}
}
