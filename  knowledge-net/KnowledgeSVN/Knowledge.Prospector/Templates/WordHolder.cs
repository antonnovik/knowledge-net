using System.Collections.Generic;
using System.Xml;

namespace Knowledge.Prospector.Templates
{
	public class WordHolder
	{
		/// <summary>
		/// All items contained in this holder.
		/// </summary>
		public WordItem[] Items;

		private string _Name;
		/// <summary>
		/// Short name of clause holder.
		/// </summary>
		public string Name
		{
			get { return _Name; }
			private set { _Name = value; }
		}

		private static readonly string XmlAttribute_Name = "Name";
		private static readonly string XmlAttribute_Word = "Word";
		private static readonly string XmlAttribute_Value = "Value";
		public WordHolder(XmlNode xmlNode)
		{
			Name = xmlNode.Attributes[XmlAttribute_Name].Value.ToUpper();
			List<WordItem> items = new List<WordItem>(xmlNode.ChildNodes.Count);
			foreach (XmlNode node in xmlNode.ChildNodes)
			{
				if(!(node is XmlComment))
					items.Add(new WordItem(
						node.Attributes[XmlAttribute_Word].Value,
						node.Attributes[XmlAttribute_Value] != null ? node.Attributes[XmlAttribute_Value].Value : string.Empty));
			}
			Items = items.ToArray();
		}

		public WordItem Find(string word)
		{
			foreach (WordItem wordItem in Items)
			{
				if (word.ToUpper() == wordItem.Word)
					return wordItem;
			}

			return null;
		}

		/// <summary>
		/// Presents one clause in holder.
		/// </summary>
		public class WordItem
		{
			/// <summary>
			/// EntityRegex used for checking this clause.
			/// </summary>
			public readonly string Word;

			/// <summary>
			/// Value what will be returned if regex will succeed.
			/// </summary>
			public readonly string Value;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="word"></param>
			/// <param name="value"></param>
			public WordItem(string word, string value)
			{
				Word = word.ToUpper();
				Value = value;
			}

			public override string ToString()
			{
				return
					(Word != null ? "Word:" + Word : string.Empty)
					+
					(Value != null ? ", Value:" + Value : string.Empty);
			}
		}
	}
}
