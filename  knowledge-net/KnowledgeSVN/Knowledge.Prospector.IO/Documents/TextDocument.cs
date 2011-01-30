using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Knowledge.Prospector.IO.Articles;

namespace Knowledge.Prospector.IO.Documents
{
	/// <summary>
	/// Each paragraph (delimited by NewLine sign) is article.
	/// </summary>
	public class TextDocument : DocumentBase, IDocument
	{
		#region TextDocument Members

		StreamReader _streamReader;

		public TextDocument(StreamReader sr)
		{
			_streamReader=sr;
		}

		private IEnumerable<IArticle> GetArticles()
		{
			long lineNo = 0;

			while (!_streamReader.EndOfStream)
			{
				string str = _streamReader.ReadLine();
				lineNo++;
				if (!str.StartsWith("//") && str.Trim() != string.Empty)
				{
					TextArticle article = new TextArticle();
					article.Info = new ArticleInfo(this.Info);
					article.Info[ArticleInfo.LineNumber] = lineNo.ToString();
					article.SourceDocument = this;
					article.Text = str + "\n";
					yield return article;
				}
			}
		}

		#endregion

		#region IEnumerable Members

		IEnumerator<IArticle> IEnumerable<IArticle>.GetEnumerator()
		{
			return GetArticles().GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return GetArticles().GetEnumerator();
		}

		#endregion

		public void Close()
		{
			_streamReader.Close();
		}
	}
}
