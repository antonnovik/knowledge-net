using System.Collections;
using System.Collections.Generic;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.IO.Documents;

namespace Knowledge.Prospector.IO.Articles
{
	/// <summary>
	/// Article, what contain string of information.
	/// </summary>
	public class TextArticle : IArticle, IEnumerable<TextBlock>
	{
		string _text = string.Empty;
		IDocument _SourceDocument;
		ArticleInfo _Info;

		#region IArticle implementation

		public string Text
		{
			get{return _text;}
			set{_text = value;}
		}
		
		public string ArticleType
		{
			get { return "Text"; }
		}

		public IDocument SourceDocument
		{
			get { return _SourceDocument; }
			set { _SourceDocument = value; }
		}

		public ArticleInfo Info
		{
			get { return _Info; }
			set { _Info = value; }
		}

		#endregion

		#region IEnumerable<TextBlock> Members

		public IEnumerator<TextBlock> GetEnumerator()
		{
			TextBlock word = null;
			TextBlockTypes currentCharType = TextBlockTypes.Unknown;
			foreach (char c in Text)
			{
				if ('0' <= c && c <= '9')
					currentCharType = TextBlockTypes.Numbers;
				else if (SeparatorEntity.IsSeparator(c))
					currentCharType = TextBlockTypes.Separator;
				else
					currentCharType = TextBlockTypes.Word;

				if (word == null)
				{
					word = new TextBlock(currentCharType, c);
				}
				else if (currentCharType == word.TextBlockType)
				{
					word.Builder.Append(c);
				}
				
				else
				{
					if (word.ToString().Trim() != string.Empty)
						yield return word;
					word = new TextBlock(currentCharType, c);
				}
			}

			if (word != null && word.ToString().Trim() != string.Empty)
				yield return word;
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
