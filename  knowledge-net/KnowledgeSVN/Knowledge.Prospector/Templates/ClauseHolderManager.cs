using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Languages;

namespace Knowledge.Prospector.Templates
{
	public class ClauseHolderManager
	{
		#region Singletone
		private static readonly ClauseHolderManager _Instance = new ClauseHolderManager();

		public static ClauseHolderManager Instance
		{
			get { return _Instance; }
		}
		#endregion

		private Dictionary<string, ClauseHolder> Holders = new Dictionary<string, ClauseHolder>();

		public ClauseHolder this[string name]
		{
			get
			{
				return Holders[name];
			}
		}

		private static readonly string XmlXPath_SelectHolders = "//ClauseHolder";
		private const string XmlAttribute_Language = "language";
		public void ReadHolders(string fileName)
		{
			ILanguage[] languages = null;

			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(PathUtils.Decode(fileName));
			XPathNavigator nav = xDoc.CreateNavigator();
			nav.MoveToRoot();
			nav.MoveToFirstChild();
			nav.MoveToFirstAttribute();
			while (nav.MoveToNextAttribute())
			{
				switch (nav.Name.ToLower())
				{
					case XmlAttribute_Language:
						languages = LanguageManager.GetInstance().GetLanguages(nav.Value.Split(','));
						break;
				}
			}
			foreach (XmlNode node in xDoc.SelectNodes(XmlXPath_SelectHolders))
				Instance.ReadHolder(node);
		}

		public void ReadHolder(XmlNode xmlNode)
		{
			ClauseHolder temp = new ClauseHolder(xmlNode);
			Holders[temp.Name] = temp;
		}

		public void Init(OneFileSettings[] options)
		{
			foreach (OneFileSettings option in options)
				if (option.Enabled)
					ReadHolders(option.FileName);
		}

		public void Init(object options)
		{
			Init(options as OneFileSettings[]);
		}
	}
}
