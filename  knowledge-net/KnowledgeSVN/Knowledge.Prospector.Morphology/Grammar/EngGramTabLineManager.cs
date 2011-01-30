using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public class EngGramTabLineManager : GramTabLineManager
	{
		#region Singletone

		private EngGramTabLineManager()
			: base(
			new Converter<string, PartOfSpeech>(EnglishPartOfSpeeches.Convert),
			new Converter<string, GrammaticalCategory>(EnglishGrammaticalCategories.Convert))

		{
		}		private static EngGramTabLineManager manager;

		public static EngGramTabLineManager GetInstance()
		{
			if (manager == null)
				manager = new EngGramTabLineManager();
			return manager;
		}

		#endregion
	}
}
