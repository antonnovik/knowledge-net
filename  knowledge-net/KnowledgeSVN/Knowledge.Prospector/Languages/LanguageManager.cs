using System.Collections.Generic;
using Knowledge.Prospector.IO.Articles;

namespace Knowledge.Prospector.Languages
{
	/// <summary>
	/// Used for languages management.
	/// </summary>
	public class LanguageManager
	{
		private static LanguageManager _instance;

		/// <summary>
		/// Return single instance of class.
		/// </summary>
		/// <returns></returns>
		public static LanguageManager GetInstance()
		{
			if (_instance == null)
			{
				_instance = new LanguageManager();
				return _instance;
			}
			else
			{
				return _instance;
			}
		}

		/// <summary>
		/// Determinate language what can be used for translating article.
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		public ILanguage DeterminateLanguage(IArticle article)
		{
			for (int i = 0; i < article.Text.Length; i++)
			{
				if (0x41 < article.Text[i] && article.Text[i] < 0x7a)
					return English.GetInstance();

				if (0x410 < article.Text[i] && article.Text[i] < 0x44f)
					return Russian.GetInstance();

			}
			return DefaultLanguage.GetInstance();
		}

		/// <summary>
		/// Determinate language what can be used for specified string.
		/// </summary>
		/// <param name="text"></param>
		/// <returns>ILanguage corresponding to text.</returns>
		public ILanguage DeterminateLanguage(string text)
		{
			if (text == null)
				return null;

			for (int i = 0; i < text.Length; i++)
			{
				if (0x41 < text[i] && text[i] < 0x7a)
					return English.GetInstance();

				if (0x410 < text[i] && text[i] < 0x44f)
					return Russian.GetInstance();

			}
			return DefaultLanguage.GetInstance();
		}

		/// <summary>
		/// Returns ILanguage by name.
		/// </summary>
		/// <param name="lang"></param>
		/// <returns>ILanguage with specified name</returns>
		public ILanguage GetLanguage(string lang)
		{
			switch (lang.Trim().ToLower())
			{
				case "russian":
					return Russian.GetInstance();
				case "english":
					return English.GetInstance();
				default:
					return DefaultLanguage.GetInstance();
			}
		}

		/// <summary>
		/// Returns array of languages by names.
		/// </summary>
		/// <param name="languages"></param>
		/// <returns>Array of ILanguage with specified names.</returns>
		public ILanguage[] GetLanguages(string[] languages)
		{
			Dictionary<ILanguage, bool> result = new Dictionary<ILanguage, bool>();
			foreach (string lang in languages)
			{
				switch (lang.Trim().ToLower())
				{
					case "russian":
						result[Russian.GetInstance()] = true;
						break;
					case "english":
						result[English.GetInstance()] = true;
						break;
					default:
						result[DefaultLanguage.GetInstance()] = true;
						break;
				}
			}
			ILanguage[] lLanguages = new ILanguage[result.Keys.Count];
			result.Keys.CopyTo(lLanguages, 0);
			return lLanguages;
		}
	}
}

