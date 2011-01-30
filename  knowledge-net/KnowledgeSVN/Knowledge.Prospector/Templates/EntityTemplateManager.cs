using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Languages;
using Knowledge.Prospector.Templates.Handlers;

namespace Knowledge.Prospector.Templates
{
	/// <summary>
	/// Manage priorities of templates.
	/// </summary>
	public class EntityTemplateManager
	{
		private List<EntityTemplate> Templates;

		private Cache<ILanguage, EntityTemplate[]> LanguageTemplatesCache;
		private Cache<EntityTemplate[], EntityTemplate[][]> OrderedTemplatesCache;

		#region SingleTone

		private static EntityTemplateManager _instance;

		public static EntityTemplateManager GetInstance()
		{
			if (_instance == null)
			{
				_instance = new EntityTemplateManager();
			}
			return _instance;
		}

		private EntityTemplateManager()
		{
			Templates = new List<EntityTemplate>();
			LanguageTemplatesCache = new Cache<ILanguage, EntityTemplate[]>(new Cache<ILanguage, EntityTemplate[]>.ObtainItemDelegate(GetTemplates));
			OrderedTemplatesCache = new Cache<EntityTemplate[], EntityTemplate[][]>(new Cache<EntityTemplate[], EntityTemplate[][]>.ObtainItemDelegate(OrderTemplates));
		}

		#endregion

		/// <summary>
		/// Adds template to manager.
		/// </summary>
		/// <param name="et"></param>
		private void AddTemplate(EntityTemplate et)
		{
			Templates.Add(et);
		}

		/// <summary>
		/// Read set of templates from XML file.
		/// </summary>
		/// <param name="fileName"></param>
		public void ReadTemplatesFile(string fileName)
		{
			ILanguage[] languages = new ILanguage[0];
			int defaultPriority = -1;
			int defaultFirstIndex = 0;

			//XmlTextReader xr = new XmlTextReader(fileName);
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(PathUtils.Decode(fileName));
			XPathNavigator nav = xDoc.CreateNavigator();
			nav.MoveToRoot();
			nav.MoveToFirstChild();
			nav.MoveToFirstAttribute();
			do
			{
				switch (nav.Name.ToUpper())
				{
					case "LANGUAGE":
						languages = LanguageManager.GetInstance().GetLanguages(nav.Value.Split(','));
						break;
					case "PRIORITY":
						defaultPriority = Convert.ToInt32(nav.Value);
						break;
					case "FIRSTINDEX":
						defaultFirstIndex = Convert.ToInt32(nav.Value);
						break;

				}
			}
			while (nav.MoveToNextAttribute());

			foreach (XmlNode node in xDoc.SelectNodes("//Shortcut"))
				ShortcutManager.Instance.Read(node);

			foreach (XmlNode node in xDoc.SelectNodes("//Holder"))
				ClauseHolderManager.Instance.ReadHolder(node);

			foreach (XmlNode node in xDoc.SelectNodes("//Template"))
				AddTemplate(new EntityTemplate(node, languages, defaultPriority, defaultFirstIndex));
		}

		/// <summary>
		/// Compare priorities of first elements from both array.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private static int CompareTemplatesPriority(EntityTemplate[] x, EntityTemplate[] y)
		{
			return x[0].Priority.CompareTo(y[0].Priority);
		}

		/// <summary>
		/// Returns templates ordered by their priorities.
		/// </summary>
		/// <param name="tempalates"></param>
		/// <returns></returns>
		private EntityTemplate[][] OrderTemplates(EntityTemplate[] tempalates)
		{
			Dictionary<int, List<EntityTemplate>> resultList = new Dictionary<int, List<EntityTemplate>>();
			foreach (EntityTemplate template in tempalates)
			{
				if (!resultList.ContainsKey(template.Priority))
					resultList[template.Priority] = new List<EntityTemplate>();

				resultList[template.Priority].Add(template);
			}

			List<EntityTemplate[]> resultArray = new List<EntityTemplate[]>();
			foreach (int priority in resultList.Keys)
			{
				resultArray.Add(resultList[priority].ToArray());
			}
			resultArray.Sort(new Comparison<EntityTemplate[]>(CompareTemplatesPriority));

			return resultArray.ToArray();
		}

		/// <summary>
		/// Apply templates corresponded to specified language.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="language"></param>
		public void ApplyTemplates(IEntityGraphBuilder builder, ILanguage language)
		{
			ApplyTemplates(builder, LanguageTemplatesCache.GetItem(language));
		}

		/// <summary>
		/// Apply tempates for specified builder.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="templates"></param>
		private void ApplyTemplates(IEntityGraphBuilder builder, EntityTemplate[] templates)
		{
			builder.OnChanged += new SourceChanged(builder_OnChanged);

			EntityTemplate[][] OrderedTemplates = OrderedTemplatesCache.GetItem(templates);

			foreach (EntityTemplate[] templatesWithSamePriority in OrderedTemplates)
			{
				builder.Reset();

				while (builder.MoveNext())
				{
					foreach (EntityTemplate template in templatesWithSamePriority)
					{
						if (AllIsOk)
						{
							template.ApplyTo(builder);
						}
					}
					if (!AllIsOk)
					{
						AllIsOk = true;
						break;
					}

				}
			}
		}

		void builder_OnChanged()
		{
			AllIsOk = false;
		}

		private bool AllIsOk = true;

		/// <summary>
		/// Returns all templates with specified language.
		/// </summary>
		/// <param name="language"></param>
		/// <returns></returns>
		private EntityTemplate[] GetTemplates(ILanguage language)
		{
			List<EntityTemplate> temp = new List<EntityTemplate>();
			foreach (EntityTemplate et in Templates)
			{
				foreach (ILanguage lang in et.Languages)
					if (lang == language)
					{
						temp.Add(et);
						break;
					}
			}
			return temp.ToArray();
		}

		#region IOptionsManaged Members

		private bool _IsInited = false;
		public bool IsInited
		{
			get { return _IsInited; }
			private set { _IsInited = value; }
		}

		public void Init(OneFileSettings[] options)
		{
			if (options == null)
				throw new NullReferenceException("options");

			foreach (OneFileSettings option in options)
				if (option.Enabled)
					ReadTemplatesFile(option.FileName);

			IsInited = true;
		}

		public void Init(object options)
		{
			Init(options as OneFileSettings[]);
		}

		#endregion
	}
}
