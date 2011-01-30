using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public class RusGramTabLineManager : GramTabLineManager
	{
		#region Singletone

		private RusGramTabLineManager()
			: base(
			new Converter<string, PartOfSpeech>(RussianPartOfSpeeches.Convert),
			new Converter<string, GrammaticalCategory>(RussianGrammaticalCategories.Convert))

		{
		}

		private static RusGramTabLineManager manager;

		public static RusGramTabLineManager GetInstance()
		{
			if (manager == null)
				manager = new RusGramTabLineManager();
			return manager;
		}

		#endregion

		#region GramTabLineManager Override

		public override bool TryReadFromString(string gramString, out GramTabLine gramLine)
		{
			if (!base.TryReadFromString(gramString, out gramLine))
				return false;

			//Applying rules!
			// неизменяемые слова как будто принадлежат всем падежам
			if (
				!((RussianGrammaticalCategories.Indeclinable & gramLine.Grammems).IsUnknown) &&
				(gramLine.PartOfSpeech != RussianPartOfSpeeches.Predicate)
				)
				gramLine.Grammems = gramLine.Grammems | RussianGrammaticalCategories.AllCases;

			if (
				!((RussianGrammaticalCategories.Indeclinable & gramLine.Grammems).IsUnknown) &&
				(gramLine.PartOfSpeech == RussianPartOfSpeeches.PronounAdjective)
				)
				gramLine.Grammems = gramLine.Grammems | RussianGrammaticalCategories.AllGenders | RussianGrammaticalCategories.AllNumbers;


			// слова общего рода ('сирота') могут  использованы как 
			// слова м.р., так и как слова ж.р.
			if (!(RussianGrammaticalCategories.MascFem & gramLine.Grammems).IsUnknown)
				gramLine.Grammems = gramLine.Grammems | RussianGrammaticalCategories.Masculinum | RussianGrammaticalCategories.Feminum;

			// слово 'пальто' не изменяется по числам, поэтому может
			// быть использовано в обоих числах
			if (gramLine.PartOfSpeech != RussianPartOfSpeeches.Predicate)
				if (!(RussianGrammaticalCategories.Indeclinable & gramLine.Grammems).IsUnknown &&
					(RussianGrammaticalCategories.Singular & gramLine.Grammems).IsUnknown)
					gramLine.Grammems = gramLine.Grammems | RussianGrammaticalCategories.Plural | RussianGrammaticalCategories.Singular;

			return true;
		}

		protected override bool ProcessAdditionalGrammem(string grammem, ref GramTabLine gramLine)
		{
			if (gramLine.PartOfSpeech == RussianPartOfSpeeches.Verb)
				if (grammem == "прч")
					gramLine.PartOfSpeech = RussianPartOfSpeeches.Participle;
				else
					if (grammem == "дпр")
						gramLine.PartOfSpeech = RussianPartOfSpeeches.AdverbParticiple;
					else
						if (grammem == "инф")
							gramLine.PartOfSpeech = RussianPartOfSpeeches.Infinitive;
			return true;
		}

		public override bool IsAdditionalGrammem(string grammem)
		{
			grammem = grammem.Trim().ToLower();
			return grammem == "прч"
				|| grammem == "дпр"
				|| grammem == "инф";
		}

		#endregion

	}
}
