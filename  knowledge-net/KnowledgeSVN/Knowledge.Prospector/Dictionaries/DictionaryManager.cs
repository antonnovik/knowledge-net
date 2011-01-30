using System;
using System.Collections.Generic;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Languages;

namespace Knowledge.Prospector.Dictionaries
{
	/// <summary>
	/// Manage dictionaries.
	/// </summary>
	/// <remarks>At one time can be loaded many dictionaries. This dictionary can translate words from language to entities or from one language to another.</remarks>
	public class DictionaryManager : ISettingsManaged<DictionaryManagerOptions>
	{
		public delegate void DictionariesChanged();
		public event DictionariesChanged OnDictionariesChanged;

		#region SingleTone

		private static DictionaryManager _instance;

		public static DictionaryManager GetInstance()
		{
			if (_instance == null)
			{
				_instance = new DictionaryManager();
			}
			return _instance;
		}

		private DictionaryManager()
		{
			_dictionaries = new List<IDictionary>();
		}

		#endregion

		#region IDictionaryManager Members

		public IEntity Translate(IDictionary[] dictionaries, string word)
		{
			if (dictionaries == null || dictionaries.Length == 0)
				return new UnknownEntity(word);
			IEntity result = new UnknownEntity(word);
			foreach (IDictionary dictionary in dictionaries)
			{
				result = dictionary.Translate(word);
				if (!(result is UnknownEntity))
					return result;
			}
			int integer;
			if (int.TryParse(word, out integer))
				return new IntegerEntity(integer);
			return result;
		}

		/// <summary>
		/// Add dictionary to collection of dictionaries.
		/// </summary>
		/// <param name="dictionary"></param>
		public void AddDictionary(IDictionary dictionary)
		{
			if (!Dictionaries.Contains(dictionary))
			{
				Dictionaries.Add(dictionary);
				if (OnDictionariesChanged != null)
					OnDictionariesChanged();
			}
		}

		private List<IDictionary> _dictionaries;
		/// <summary>
		/// Gets collection of dictionaries.
		/// </summary>
		private List<IDictionary> Dictionaries
		{
			get
			{
				return _dictionaries;
			}
		}

		public IDictionary[] GetDictionaries(ILanguage language)
		{
			List<IDictionary> list = new List<IDictionary>();
			foreach (IDictionary dictionary in Dictionaries)
			{
				foreach (ILanguage lang in dictionary.Languages)
					if (lang.Name == language.Name)
					{
						list.Add(dictionary);
						break;
					}
			}
			return list.ToArray();
		}

		public IEntity Translate(ILanguage language, string word)
		{
			return Translate(GetDictionaries(language), word);
		}

		#endregion

		#region IOptionsManaged Members

		private bool _IsInited = false;
		public bool IsInited
		{
			get { return _IsInited; }
			private set { _IsInited = value; }
		}

		public void Init(DictionaryManagerOptions options)
		{
			if (options == null)
				throw new NullReferenceException("XmlDictionaryOptions");

			foreach (OneFileSettings xmlDictionaryOptions in options.XmlDictionariesOptions)
			{
				if (xmlDictionaryOptions.Enabled)
				{
					XmlDictionary dictionary = new XmlDictionary();
					dictionary.Init(xmlDictionaryOptions);
					AddDictionary(dictionary);
				}
			}
			foreach (MrdDictionaryAdapterOptions mrdDictionaryOptions in options.MrdDictionariesOptions)
			{
				if (mrdDictionaryOptions.Enabled)
				{
					MrdDictionaryAdapter dictionary = new MrdDictionaryAdapter();
					dictionary.Init(mrdDictionaryOptions);
					AddDictionary(dictionary);
				}
			}
			IsInited = true;
		}

		#endregion
	}

}
