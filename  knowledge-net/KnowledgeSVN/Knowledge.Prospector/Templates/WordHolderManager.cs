using System.Collections.Generic;
using System.Xml;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.Templates
{
	public class WordHolderManager
	{
		#region Singletone
		private static readonly WordHolderManager _Instance = new WordHolderManager();

		public static WordHolderManager Instance
		{
			get { return _Instance; }
		}
		#endregion

		private Dictionary<string, WordHolder> Holders = new Dictionary<string, WordHolder>();

		public WordHolder this[string name]
		{
			get
			{
				return Holders[name];
			}
		}

		private static readonly string XmlXPath_SelectHolders = "//WordHolder";
		public void ReadHolders(string fileName)
		{
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(PathUtils.Decode(fileName));
			foreach (XmlNode node in xDoc.SelectNodes(XmlXPath_SelectHolders))
				Instance.ReadHolder(node);
		}

		public void ReadHolder(XmlNode xmlNode)
		{
			WordHolder temp = new WordHolder(xmlNode);
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
