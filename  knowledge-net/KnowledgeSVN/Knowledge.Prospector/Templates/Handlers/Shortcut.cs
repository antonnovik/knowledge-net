using System.Reflection;
using System.Xml;

namespace Knowledge.Prospector.Templates.Handlers
{
	/// <summary>
	/// Shortcut for full name of handler used in <see cref="Knowledge.Prospector.Templates.EntityTemplate" />
	/// </summary>
	public class Shortcut
	{
		private string _Name;
		/// <summary>
		/// Short name of shortcut.
		/// </summary>
		public string Name
		{
			get { return _Name; }
			private set { _Name = value; }
		}

		private string _FullTypeName;
		/// <summary>
		/// Full name of shortcut.
		/// </summary>
		public string FullTypeName
		{
			get { return _FullTypeName; }
			private set { _FullTypeName = value; }
		}

		private ConstructorInfo _Handler;

		/// <summary>
		/// Used for initializing new instance of referenced handler.
		/// </summary>
		/// <returns>New instance of referenced handler.</returns>
		public object GetHandler()
		{
			return _Handler.Invoke(new object[0]);
		}

		public Shortcut(XmlNode xmlNode)
		{
			Name = xmlNode.Attributes["Name"].Value;
			FullTypeName = xmlNode.Attributes["Class"].Value;
			_Handler = ShortcutManager.GetConstructorInfo(FullTypeName);
		}
	}
}
