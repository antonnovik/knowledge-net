using System;
using System.Reflection;
using System.Xml;

namespace Knowledge.Prospector.Common.Settings
{
	/// <summary>
	/// Provider class for T.
	/// </summary>
	/// <typeparam name="T">Type of provided class.</typeparam>
	public class Provider<T> where T : class
	{
		private string _Name;
		private string _TypeName;
		private XmlNode _XmlSection;
		private string _PluginTypeName;

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public string TypeName
		{
			get { return _TypeName; }
			set { _TypeName = value; }
		}

		public XmlNode XmlSection
		{
			get { return _XmlSection; }
			set { _XmlSection = value; }
		}

		public string PluginTypeName
		{
			get { return _PluginTypeName; }
			set { _PluginTypeName = value; }
		}

		public T CreateProvidedClassInstance()
		{
			Type type = Type.GetType(TypeName);
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			if (constructor == null)
				throw new Exception("Provided class must have empty constructor.");
			object ontology = constructor.Invoke(new object[0]);
			if (!(ontology is T))
				throw new Exception(string.Format("Type \"{0}\" must implement IOntology interface", TypeName));
			return ontology as T;
		}

		public IProviderOptionsPlugin CreatePluginInstance()
		{
			Type type = Type.GetType(PluginTypeName);
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			if (constructor == null)
				throw new Exception("Provided class must have empty constructor.");
			return constructor.Invoke(new object[0]) as IProviderOptionsPlugin;
		}

	}
}
