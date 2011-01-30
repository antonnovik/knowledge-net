using System.Collections.Generic;
using System.Xml;
using Knowledge.Prospector.Languages;
using Knowledge.Prospector.Templates.EntityExpressions;

namespace Knowledge.Prospector.Templates
{
	/// <summary>
	/// Holds several clauses as one. Can be used in templates.
	/// </summary>
	public class ClauseHolder
	{
		/// <summary>
		/// All items contained in this holder.
		/// </summary>
		public ClauseItem[] Items;

		private string _Name;
		/// <summary>
		/// Short name of clause holder.
		/// </summary>
		public string Name
		{
			get { return _Name; }
			private set { _Name = value; }
		}

		private ILanguage[] _Languages;
		/// <summary>
		/// Language of this template.
		/// </summary>
		public ILanguage[] Languages
		{
			get { return _Languages; }
			private set { _Languages = value; }
		}

		private static readonly string XmlAttribute_Name = "Name";
		private static readonly string XmlAttribute_Language = "Language";
		private static readonly string XmlAttribute_Pattern = "Pattern";
		private static readonly string XmlAttribute_Value = "Value";
		private static readonly string XmlAttribute_Index = "Index";
		public ClauseHolder(XmlNode xmlNode)
		{
			Name = xmlNode.Attributes[XmlAttribute_Name].Value.ToUpper();
			string strLanguages = xmlNode.Attributes[XmlAttribute_Language] != null ? xmlNode.Attributes[XmlAttribute_Language].Value.Trim().ToUpper() : string.Empty;
			Languages = strLanguages != string.Empty ? LanguageManager.GetInstance().GetLanguages(strLanguages.Split(',')) : null;
			List<ClauseItem> items = new List<ClauseItem>(xmlNode.ChildNodes.Count);
			foreach (XmlNode node in xmlNode.ChildNodes)
			{
				if(!(node is XmlComment))
					items.Add(new ClauseItem(
						node.Attributes[XmlAttribute_Pattern].Value,
						node.Attributes[XmlAttribute_Value] != null ? node.Attributes[XmlAttribute_Value].Value : string.Empty,
						node.Attributes[XmlAttribute_Index] != null ? int.Parse(node.Attributes[XmlAttribute_Index].Value) : 0));
			}
			Items = items.ToArray();
		}

		/// <summary>
		/// Presents one clause in holder.
		/// </summary>
		public class ClauseItem
		{
			/// <summary>
			/// EntityRegex used for checking this clause.
			/// </summary>
			public readonly EntityRegex Regex;

			/// <summary>
			/// Index of returned entity if regex will be succeed.
			/// </summary>
			public readonly int Index = 0;

			/// <summary>
			/// Value what will be returned if regex will succeed.
			/// </summary>
			public readonly string Value;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="regex"></param>
			/// <param name="value"></param>
			/// <param name="index"></param>
			public ClauseItem(string regex, string value, int index)
			{
				Regex = new EntityRegex(regex);
				Index = index;
				Value = value;
			}

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="regex"></param>
			/// <param name="value"></param>
			public ClauseItem(string regex, string value)
				: this(regex, value, 0)
			{ }

			public override string ToString()
			{
				return 
					(Regex != null ? "Regex:" + Regex.ToString() : string.Empty)
					+
					(Value != null ? ", Value:" + Value : string.Empty)
					+
					(", Index:" + Index);
			}
		}
	}
}
