using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public abstract class GramTabLineManager
	{
		private Converter<string, PartOfSpeech> _partOfSpeecheConverter;
		private Converter<string, GrammaticalCategory> _gammaticalCategoriesConverter;

		protected GramTabLineManager(Converter<string, PartOfSpeech> partOfSpeecheConverter, Converter<string, GrammaticalCategory> gammaticalCategoriesConverter)
		{
			_partOfSpeecheConverter = partOfSpeecheConverter;
			_gammaticalCategoriesConverter = gammaticalCategoriesConverter;
		}
		public virtual bool TryReadFromString(string gramString, out GramTabLine gramLine)
		{
			gramLine = new GramTabLine();

			if (gramString == null
				|| (gramString = gramString.Trim()) == string.Empty)
				throw new Exception("Non empty string expected");

			//Reading part of speach
			int indexOfSpace = gramString.IndexOf(' ');
			indexOfSpace = indexOfSpace != -1 ? indexOfSpace : gramString.Length;
			string partOfSpeach = gramString.Substring(0, indexOfSpace);

			gramLine.PartOfSpeech = _partOfSpeecheConverter(partOfSpeach);

			//Moving "iterator"
			gramString = gramString.Substring(indexOfSpace).Trim();

			if (gramString == string.Empty)
				return true;

			//Reading grammems
			foreach (string grammem in gramString.Split(new char[] { ',' }))
			{
				string temp = grammem.Trim();
				if (_gammaticalCategoriesConverter(temp) != GrammaticalCategory.Unknown)
					gramLine.Grammems = gramLine.Grammems | _gammaticalCategoriesConverter(temp);
				else if (IsAdditionalGrammem(temp))
				{
					ProcessAdditionalGrammem(temp, ref gramLine);
				}
				else return false;// throw new Exception("Unsupported Grammatic category");
			}
			return true;
		}

		public virtual bool IsAdditionalGrammem(string grammem)
		{
			return false;
		}

		protected virtual bool ProcessAdditionalGrammem(string grammem, ref GramTabLine gramLine)
		{
			return false;
		}
	}
}
