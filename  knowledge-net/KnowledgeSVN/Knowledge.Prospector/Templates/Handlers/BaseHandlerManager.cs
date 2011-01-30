using System.Collections.Generic;
using System.Xml;

namespace Knowledge.Prospector.Templates.Handlers
{
	public static class BaseHandlerManager
	{
		public static IBaseHandler[] SplitHandlers(XmlNode xmlNode)
		{
			List<IBaseHandler> handlers = new List<IBaseHandler>();
			foreach (XmlNode node in xmlNode.SelectNodes("./Handler"))
			{
				IBaseHandler temp = null;
				string name = node.Attributes["Name"].Value;
				temp = ShortcutManager.CreateHandler(name) as IBaseHandler;
				temp.Read(node);
				handlers.Add(temp);
			}
			return handlers.ToArray();
		}
	}
}
