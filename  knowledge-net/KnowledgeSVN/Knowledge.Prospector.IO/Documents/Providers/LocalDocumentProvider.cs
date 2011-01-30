using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.IO.Documents.Providers
{
	public class LocalDocumentProvider : DocumentProviderBase, IDocumentProvider
	{
		protected override IEnumerable<IDocument> GetDocuments()
		{
			XmlNode xmlConfig = XmlUtils.GetNode(_Config);
			string folder = xmlConfig.SelectSingleNode(".//folder").InnerText;
			string filter = xmlConfig.SelectSingleNode(".//filter").InnerText;

			string[] fileNames = Directory.GetFiles(PathUtils.Decode(folder), filter, SearchOption.AllDirectories);
			if (fileNames != null)
			{
				foreach (string fileName in fileNames)
				{
					yield return DocumentFactory.Instance.GetDocument(fileName);
				}
			}
		}
	}
}
