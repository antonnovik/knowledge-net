using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Dictionaries;

namespace Knowledge.Prospector.Languages
{
	public abstract class Language : ILanguage
	{
		public abstract string Name { get;}

		private IDictionary[] _dictionaries;
		public IDictionary[] Dictionaries
		{
			get
			{
				return _dictionaries;
			}
			protected set
			{
				_dictionaries = value;
			}
		}

		protected Language()
		{
			DictionaryManager.GetInstance().OnDictionariesChanged += new DictionaryManager.DictionariesChanged(Language_OnDictionariesChanged);
		}

		public void Language_OnDictionariesChanged()
		{
			ResetDictionaries();
		}


		public override string ToString()
		{
			return string.Format("{0} language", Name);
		}

		#region ILanguage Members

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Language lang = obj as Language;
			if (lang == null)
				return false;
			else
				return lang.Name == Name;
		}

		public IEntity Translate(string word)
		{
			return DictionaryManager.GetInstance().Translate(Dictionaries, word);
		}

		public void ResetDictionaries()
		{
			Dictionaries = DictionaryManager.GetInstance().GetDictionaries(this);
		}

		#endregion
	}
}
