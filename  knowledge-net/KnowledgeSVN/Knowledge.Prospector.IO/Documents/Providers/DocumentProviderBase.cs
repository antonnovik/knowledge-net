using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Knowledge.Prospector.IO.Documents.Providers
{
	public abstract class DocumentProviderBase : IDocumentProvider
	{
		protected string _Config;

		protected abstract IEnumerable<IDocument> GetDocuments();

		#region IDocumentProvider Members

		public void Init(string config)
		{
			_Config = config;
		}

		public void Close()
		{

		}

		#endregion

		#region IEnumerable Members

		IEnumerator<IDocument> IEnumerable<IDocument>.GetEnumerator()
		{
			return GetDocuments().GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return GetDocuments().GetEnumerator();
		}

		#endregion

	}
}
