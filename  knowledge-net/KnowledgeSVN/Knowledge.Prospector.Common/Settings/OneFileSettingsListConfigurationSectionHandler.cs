using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Knowledge.Prospector.Common.Settings
{
	public class OneFileSettingsListConfigurationSectionHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler Members
		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
		{
			List<OneFileSettings> result = new List<OneFileSettings>();

			foreach (XmlNode child in section.ChildNodes)
			{
				if (XmlNodeType.Element == child.NodeType)
				{
					result.Add(CreateOneFileOptions(child));
				}
			}

			return result.ToArray();
		}
		#endregion

		/// <summary>
		/// Reads OneFileOption node.
		/// </summary>
		/// <param name="section"></param>
		/// <returns></returns>
		private OneFileSettings CreateOneFileOptions(XmlNode section)
		{
			OneFileSettings result = new OneFileSettings();
			if(section.Attributes["Enabled"] != null)
				result.Enabled = Convert.ToBoolean(section.Attributes["Enabled"].Value);
			if(section.Attributes["Language"] != null)
				result.Language = section.Attributes["Language"].Value;
			if (section.Attributes["Name"] != null)
				result.FileName = section.Attributes["Name"].Value;
			return result;
		}
	}
}
