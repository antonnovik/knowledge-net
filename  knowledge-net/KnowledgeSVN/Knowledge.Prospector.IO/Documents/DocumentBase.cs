using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.Prospector.IO.Documents
{
	public class DocumentBase
	{
		private DocumentInfo _Info;

		public DocumentInfo Info
		{
			get { return _Info; }
			set { _Info = value; }
		}
		
		public DocumentBase()
		{
			_Info = new DocumentInfo();
		}
	}
}
