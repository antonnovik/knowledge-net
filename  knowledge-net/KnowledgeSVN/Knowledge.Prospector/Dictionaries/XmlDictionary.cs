using System;
using System.Xml;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Data.Relationships;
using Knowledge.Prospector.Languages;

namespace Knowledge.Prospector.Dictionaries
{
	/// <summary>
	/// Xml dictionary
	/// </summary>
	public class XmlDictionary : IDictionary, ISettingsManaged<OneFileSettings>
	{
		private XmlDocument xmlDocument;

		protected void Init(string fileName)
		{
			xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
		}

		#region Working with Xml

		protected XmlNodeList FindLexical(string value)
		{
			return xmlDocument.SelectNodes("dictionary/lexical[@value=\"" + value.ToUpper() + "\"]");
		}

		protected XmlNodeList FindEntity(string value)
		{
			return xmlDocument.SelectNodes("dictionary/entity[@value=\"" + value.ToUpper() + "\"]");
		}

		protected XmlNodeList FindLexical(XmlNode xmlNode, string value)
		{
			return xmlNode.SelectNodes("//lexical[@value=\"" + value.ToUpper() + "\"]");
		}

		protected XmlNodeList FindEntity(XmlNode xmlNode, string value)
		{
			return xmlNode.SelectNodes("//entity[@value=\"" + value.ToUpper() + "\"]");
		}


		/// <summary>
		/// Return all inner "type" elements. For example &lt;root&gt; &lt;type ... /&gt; &lt;/root&gt;
		/// </summary>
		/// <param name="xmlNode"></param>
		/// <returns></returns>
		private XmlNodeList FindTypeElements(XmlNode xmlNode)
		{
			return xmlNode.SelectNodes(".//type");
		}

		protected XmlNodeList FindLexicalByRoot(string value)
		{
			return xmlDocument.SelectNodes("dictionary//lexical[element/root[contains(\"" + value + "\", @value)]]");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		protected XmlNodeList FindLexicalClassBySuffix(string value)
		{
			if (value.IndexOfAny(new char[] { ' ', '\t' }) == -1)
				return xmlDocument.SelectNodes("//lexical[@class][.//suffix[@value][string-length(substring-after(@value, \"" + value + "\"))=0]]");
			else
				return null;
		}

		/// <summary>
		/// Return all inner elements with haven't elements and attributes. For example &lt;root&gt; &lt;???/&gt; &lt;/root&gt;
		/// </summary>
		/// <param name="xmlNode"></param>
		/// <returns></returns>
		/// <remarks>Characteristics = "symmetric", "transitive"...</remarks>
		protected XmlNodeList FindRelationshipEntityKinds(XmlNode xmlNode)
		{
			return xmlNode.SelectNodes(".//*[count(@*)=0][count(*)=0]");
		}

		/// <summary>
		/// Return inner "type" attribute value. For example &lt;root type="..."&gt;
		/// </summary>
		/// <param name="xmlNode">Type of relationship entity xml node.</param>
		/// <returns></returns>
		protected string FindRelationshipEntityType(XmlNode xmlNode)
		{
			if (xmlNode.Attributes["type"] != null)
				return xmlNode.Attributes["type"].Value;
			else
				return string.Empty;
		}

		#endregion

		RelationshipEntity CreateRelationshipEntity(string word, XmlNode xmlNode)
		{
			RelationshipEntity relationshipEntity = new RelationshipEntity(word);
			XmlNodeList types = FindRelationshipEntityKinds(xmlNode);
			if (types.Count != 0)
			{
				foreach (XmlNode type in types)
				{
					switch (type.Name.ToLower())
					{
						case "symmetric":
							relationshipEntity.Kind |= RelationshipKind.Symmetric;
							break;
						case "transitive":
							relationshipEntity.Kind |= RelationshipKind.Transitive;
							break;
						case "functional":
							relationshipEntity.Kind |= RelationshipKind.Functional;
							break;
					}
				}
			}
			relationshipEntity.Type = FindRelationshipEntityType(xmlNode);
			return relationshipEntity;
		}

		#region IDictionary Members

		public IEntity Translate(string word)
		{
			try
			{
				XmlNodeList xmlNodeList = FindEntity(word);
				if (xmlNodeList.Count == 0)
					xmlNodeList = FindLexical(word);
				//		if (xmlNodeList.Count == 0)
				//			xmlNodeList = FindLexicalByRoot(word);
				//		if (xmlNodeList.Count == 0)
				//			xmlNodeList = FindLexicalClassBySuffix(word);
				if (xmlNodeList == null || xmlNodeList.Count == 0)
					return new UnknownEntity(word);
				else
				{
					switch (xmlNodeList[0].Attributes["class"].Value.ToLower())
					{
						case "noun":
							return new ClassEntity(word);
						case "class":
							return new ClassEntity(word);
						case "adjective":
							return new PropertyEntity(word);
						case "property":
							return new PropertyEntity(word);
						case "relationship":
							return CreateRelationshipEntity(word, xmlNodeList[0]);
						default:
							return new UnknownEntity(word);
					}
				}
			}
			catch
			{
				return new UnknownEntity(word);
			}
		}

		private ILanguage[] languages;
		public ILanguage[] Languages
		{
			get { return languages; }
		}
		
		#endregion

		#region IOptionsManaged Members

		private bool _IsInited = false;
		public bool IsInited
		{
			get { return _IsInited; }
			private set { _IsInited = value; }
		}

		public void Init(OneFileSettings options)
		{
			if (options == null)
				throw new NullReferenceException("XmlDictionaryOptions");

			Init(options.FileName);
			languages = new ILanguage[] { LanguageManager.GetInstance().GetLanguage(options.Language) };

			IsInited = true;
		}

		#endregion
}
}
