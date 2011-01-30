using System.Xml;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Languages;
using Knowledge.Prospector.Templates.EntityExpressions;
using Knowledge.Prospector.Templates.Handlers;

namespace Knowledge.Prospector.Templates
{
	/// <summary>
	/// Return true if list has format as specified in string.
	/// </summary>
	/// <param name="format"></param>
	/// <returns></returns>
	public class EntityTemplate
	{
		public IBaseHandler[] Handlers;
		public EntityRegex MainRegex;
		public int Priority;

		private ILanguage[] _languages;
		/// <summary>
		/// All languages for with this template serve.
		/// </summary>
		public ILanguage[] Languages
		{
			get
			{
				return _languages;
			}
			private set
			{
				_languages = value;
			}
		}

		public EntityTemplate(XmlNode xmlNode, ILanguage[] languages)
		{
			Languages = languages;

			Priority = int.Parse(xmlNode.Attributes["Priority"].Value);
			MainRegex = new EntityRegex(xmlNode.Attributes["Pattern"].Value);

			Handlers = BaseHandlerManager.SplitHandlers(xmlNode);
		}

		public EntityTemplate(XmlNode xmlNode, ILanguage[] languages, int defaultPriority, int defaultFirstIndex)
		{
			Languages = languages;

			if (xmlNode.Attributes["Priority"] != null)
				Priority = int.Parse(xmlNode.Attributes["Priority"].Value);
			else
				Priority = defaultPriority;
			MainRegex = new EntityRegex(xmlNode.Attributes["Pattern"].Value);

			Handlers = BaseHandlerManager.SplitHandlers(xmlNode);
		}

#if DEBUG
		public override string ToString()
		{
			return _ToString;
		}
		private string _ToString = string.Empty;
#else
		public override string ToString()
		{
			return string.Format("Priority: {0}, RegEx: {1}, Handlers: {2}", Priority, MainRegex, Handlers.Length);
		}
#endif

		public void ApplyTo(IEntityGraphBuilder builder)
		{
			EntityRegexMatchInfo info = MainRegex.Interpret(builder.Source.ToArray(), builder.Index);
			if (info.States != null && info.States.Count != 0)
			{
				foreach(IBaseHandler handler in Handlers)
					handler.Execute(builder, info);
			}
		}
	}
}
