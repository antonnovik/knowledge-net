using System;

namespace Knowledge.Prospector.Morphology.Grammar
{
	[Serializable]
	public class GramTabLine
	{
		PartOfSpeech _partOfSpeech;
		GrammaticalCategory _grammaticalCategory;

		public PartOfSpeech PartOfSpeech
		{
			get
			{
				return _partOfSpeech;
			}
			set
			{
				_partOfSpeech = value;
			}
		}

		public GrammaticalCategory Grammems
		{
			get
			{
				return _grammaticalCategory;
			}
			set
			{
				_grammaticalCategory = value;
			}
		}
	}
}
