using Knowledge.Prospector.Data.Entities;
using Knowledge.Prospector.Dictionaries;
using Knowledge.Prospector.IO.Articles;
using Knowledge.Prospector.Languages;

namespace Knowledge.Prospector
{
	public sealed class EntityManager
	{
		private static readonly EntityManager _Instance = new EntityManager();

		public static EntityManager Instance
		{
			get
			{
				return _Instance;
			}
		}

		private EntityManager()
		{
			
		}

		public IEntity CreateEntity(string word, TextBlockTypes attribute)
		{
			if (word == string.Empty)
				return null;

			switch (attribute)
			{
				case TextBlockTypes.Numbers:
					return new IntegerEntity(int.Parse(word));
				case TextBlockTypes.Separator:
					return SeparatorEntity.GetSeparator(word.Trim());
				case TextBlockTypes.Word:
					return DictionaryManager.GetInstance().Translate(LanguageManager.GetInstance().DeterminateLanguage(word), word);
				default:
					return null;
			}
		}

		public IEntity CreateEntity(TextBlock word)
		{
			return CreateEntity(word.ToString(), word.TextBlockType);
		}
	}
}
