using System.Configuration;
using System.Xml;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.Common.Settings
{
	public class ProviderConfigurationSectionHandler<T> : IConfigurationSectionHandler where T : class
	{
		#region IConfigurationSectionHandler Members

		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
		{
			ProviderConfiguration<T> result = new ProviderConfiguration<T>();

			foreach (XmlNode child in section.ChildNodes)
			{
				if (XmlNodeType.Element == child.NodeType)
				{
					Provider<T> temp = ReadProviderInformation(child);
					if (temp != null)
						result.Providers.Add(temp);
				}
			}

			return result;

		}
		#endregion

		/// <summary>
		/// Reads OneFileOption node.
		/// </summary>
		/// <param name="section"></param>
		/// <returns></returns>
		private Provider<T> ReadProviderInformation(XmlNode section)
		{
			AssertUtils.AssertParamNotNull(section, "section");
			AssertUtils.AssertXmlAttributRequired(section, "name");
			AssertUtils.AssertXmlAttributRequired(section, "type");

			Provider<T> result = new Provider<T>();

			result.XmlSection = section;
			result.Name = section.Attributes["name"].Value;
			result.TypeName = section.Attributes["type"].Value;
			if(section.Attributes["pluginType"] != null)
				result.PluginTypeName = section.Attributes["pluginType"].Value;
			
			return result;
		}
	}
}
