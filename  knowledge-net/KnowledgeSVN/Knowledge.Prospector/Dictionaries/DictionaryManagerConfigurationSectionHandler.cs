using System;
using System.Configuration;
using System.Xml;
using Knowledge.Prospector.Common.Settings;

namespace Knowledge.Prospector.Dictionaries
{
	class DictionaryManagerConfigurationSectionHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler Members

		object IConfigurationSectionHandler.Create(
		  object parent, object configContext, XmlNode section)
		{
			DictionaryManagerOptions result = new DictionaryManagerOptions();

			foreach (XmlNode child in section.ChildNodes)
			{
				if (XmlNodeType.Element == child.NodeType)
				{
					switch(child.Name)
					{
						case "XmlDictionary":
							result.XmlDictionariesOptions.Add(CreateXmlDictionaryOptions(child));
							break;
						case "MrdDictionary":
							result.MrdDictionariesOptions.Add(CreateMrdDictionaryOptions(child));
							break;
					}
				}
			}

			return result;
		}
		#endregion

		private OneFileSettings CreateXmlDictionaryOptions(XmlNode section)
		{
			OneFileSettings result = new OneFileSettings();
			if (section.Attributes["Enabled"] != null)
				result.Enabled = Convert.ToBoolean(section.Attributes["Enabled"].Value);
			if (section.Attributes["Language"] != null)
				result.Language = section.Attributes["Language"].Value;
			if (section.Attributes["Name"] != null)
				result.FileName = section.Attributes["Name"].Value;
			return result;
		}

		private MrdDictionaryAdapterOptions CreateMrdDictionaryOptions(XmlNode section)
		{
			MrdDictionaryAdapterOptions result = new MrdDictionaryAdapterOptions();
			if (section.Attributes["Enabled"] != null)
				result.Enabled = Convert.ToBoolean(section.Attributes["Enabled"].Value);
			if (section.Attributes["Language"] != null)
				result.Language = section.Attributes["Language"].Value;

			foreach(XmlNode child in section.ChildNodes)
			{
				switch(child.Name)
				{
					case "MrdFile":
						result.MrdFileName = child.Attributes["Name"].Value;
						break;
					case "GramTabFile":
						result.GramTabFileName = child.Attributes["Name"].Value;
						break;
				}
			}
			return result;
		}
	}
}
