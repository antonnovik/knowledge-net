using System.Text;

namespace Knowledge.Prospector.IO.Articles
{
	public class TextBlock
	{
		private TextBlockTypes _TextBlockType;
		public TextBlockTypes TextBlockType
		{
			get { return _TextBlockType; }
			private set { _TextBlockType = value; }
		}

		private StringBuilder _Builder;
		public StringBuilder Builder
		{
			get { return _Builder; }
			private set { _Builder = value; }
		}

		public TextBlock(TextBlockTypes textBlockType)
		{
			TextBlockType = textBlockType;
			Builder = new StringBuilder(20);
		}

		public TextBlock(TextBlockTypes textBlockType, StringBuilder builder)
		{
			TextBlockType = textBlockType;
			Builder = builder;
		}

		public TextBlock(TextBlockTypes textBlockType, char firstChar)
			: this(textBlockType)
		{
			Builder.Append(firstChar);
		}

		public override string ToString()
		{
			return Builder.ToString();
		}
	}
}
