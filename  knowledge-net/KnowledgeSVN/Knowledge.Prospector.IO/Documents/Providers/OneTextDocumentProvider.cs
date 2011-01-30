using System.Collections.Generic;
using System.Xml;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.IO.Documents.Providers
{
	/// <summary>
	/// Provide access only to one Text Document
	/// </summary>
	public class OneTextDocumentProvider : DocumentProviderBase, IDocumentProvider
	{
		protected override IEnumerable<IDocument> GetDocuments()
		{
			XmlNode xmlConfig = XmlUtils.GetNode(_Config);
			string fileName = xmlConfig.SelectSingleNode(".//file").InnerText;

			yield return DocumentFactory.Instance.GetDocument(PathUtils.Decode(fileName));
		}
	}
}
