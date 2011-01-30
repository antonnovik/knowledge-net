using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.IO.Documents;

namespace Knowledge.Prospector.IO.Articles
{
	public class ArticleInfo : DocumentInfo
	{
		public const string LineNumber = "LineNumber";

		public ArticleInfo()
		{}

		public ArticleInfo(DocumentInfo documentInfo)
		{
			AssertUtils.AssertParamNotNull(documentInfo, "documentInfo");

			for (int i = 0; i < documentInfo.Count; i++)
				this[documentInfo.GetKey(i)] = documentInfo[i];
		}

	}
}
