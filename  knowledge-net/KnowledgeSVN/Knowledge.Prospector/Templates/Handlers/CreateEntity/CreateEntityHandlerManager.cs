using System.Xml;

namespace Knowledge.Prospector.Templates.Handlers.CreateEntity
{
	public static class CreateEntityHandlerManager
	{
		public static ICreateEntityHandler GetHandler(XmlNode xmlNode)
		{
			XmlNode node = xmlNode.SelectSingleNode("./CreateEntityHandler");
			ICreateEntityHandler temp = null;
			string name = node.Attributes["Name"].Value;
			temp = ShortcutManager.CreateHandler(name) as ICreateEntityHandler;
			temp.Read(node);
			return temp;
		}
	}
}
