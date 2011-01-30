using System;
using Knowledge.Prospector.Common;
using Knowledge.Prospector.IO.Articles;

namespace Knowledge.Prospector
{
	public class ProspectorManager
	{
		#region Singletone
		private static readonly ProspectorManager _Instance = new ProspectorManager();

		public static ProspectorManager Instance
		{
			get
			{
				return _Instance;
			}
		}

		private ProspectorManager()
		{
			_TranslatorsCache = new Cache<string, IProspector>(new Cache<string, IProspector>.ObtainItemDelegate(ObtainTranslator));
		}
		#endregion

		private Cache<string, IProspector> _TranslatorsCache;

		private IProspector ObtainTranslator(string articleTypeName)
		{
			if (string.Compare("Text", articleTypeName, true) == 0)
				return new TextProspector();
			else
				throw new Exception(string.Format("Unknown article type: {0}", articleTypeName));
		}

		public IProspector GetProspector(IArticle article)
		{
			return _TranslatorsCache.GetItem(article.ArticleType);
		}
	}
}
