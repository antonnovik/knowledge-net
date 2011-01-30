using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.Templates.Handlers
{
	/// <summary>
	/// Manager class used for creating new instance of Handler classes used in templates.
	/// </summary>
	public class ShortcutManager
	{
		#region SingleTone
		private static readonly ShortcutManager _Instance = new ShortcutManager();

		public static ShortcutManager Instance
		{
			get { return _Instance; }
		}
		#endregion

		private Dictionary<string, Shortcut> Shortcuts = new Dictionary<string, Shortcut>();

		public Shortcut this[string name]
		{
			get
			{
				return Shortcuts[name];
			}
		}

		public bool Contains(string name)
		{
			return Shortcuts.ContainsKey(name);
		}

		private static readonly string XmlXPath_SelectShortcuts = "//Shortcut";
		private const string XmlAttribute_Language = "language";
		public void Read(string fileName)
		{
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(PathUtils.Decode(fileName));
			XPathNavigator nav = xDoc.CreateNavigator();
			nav.MoveToRoot();
			foreach (XmlNode node in xDoc.SelectNodes(XmlXPath_SelectShortcuts))
				Instance.Read(node);
		}

		public void Read(XmlNode xmlNode)
		{
			Shortcut temp = new Shortcut(xmlNode);
			Shortcuts[temp.Name] = temp;
		}

		internal static ConstructorInfo GetConstructorInfo(string fullTypeName)
		{
			Type type = Type.GetType(fullTypeName);
			return type.GetConstructor(new Type[0]);
		}

		public static object CreateHandler(string name)
		{
			if (Instance.Contains(name))
				return Instance[name].GetHandler();
			else
				return GetConstructorInfo(name).Invoke(new object[0]);
		}

		public void Init(OneFileSettings[] options)
		{
			foreach (OneFileSettings option in options)
				if (option.Enabled)
					Read(option.FileName);
		}

		public void Init(object options)
		{
			Init(options as OneFileSettings[]);
		}
	}
}
